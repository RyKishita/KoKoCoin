using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using MemoryPack;

namespace Assets.Scripts.Duel.AreaStatus
{
    [MemoryPackable]
    public partial class AreaStatusList : ICloneable, IEquatable<AreaStatusList>, IEqualityComparer<AreaStatusList>
    {
        [MemoryPackConstructor]
        public AreaStatusList()
        {

        }

        public AreaStatusList(AreaStatusList src)
        {
            Items = src.Items.Select(item => item.Duplicate()).ToList();
        }

        #region serialize

        public List<IAreaStatus> Items = new List<IAreaStatus>();

        #endregion

        public void Regist(IAreaStatus status)
        {
            var findItem = Items.FirstOrDefault(item => status.IsMatch(item));
            if (null == findItem)
            {
                Items.Add(status);
            }
            else
            {
                findItem.Marge(status);
            }
        }

        public bool IsEmpty()
        {
            return !Items.Any();
        }

        public void Clear()
        {
            Items.Clear();
        }

        public void RemoveBy<T>() where T : IAreaStatus
        {
            Items.RemoveAll(item => item is T);
        }

        public void RemoveBy(IAreaStatus status)
        {
            Items.RemoveAll(item => status.IsMatch(item));
        }

        public T GetItem<T>() where T : IAreaStatus
        {
            return Items.Where(item => item is T).Cast<T>().FirstOrDefault();
        }

        public IAreaStatus GetItem(Type type)
        {
            return Items.Where(item => item.GetType() == type).FirstOrDefault();
        }

        public bool Has<T>() where T : IAreaStatus
        {
            return Items.Any(item => item is T);
        }

        public bool Has(IAreaStatus status)
        {
            return Items.Any(item => status.IsMatch(item));
        }

        public IEnumerable<string> MakeExplainTexts()
        {
            foreach (var item in Items)
            {
                yield return item.MakeExplain();
            }
        }

        public object Clone()
        {
            return Duplicate();
        }

        public AreaStatusList Duplicate()
        {
            return new AreaStatusList(this);
        }

        public bool Equals(AreaStatusList other)
        {
            if (other == null) return false;
            if (ReferenceEquals(other, this)) return true;
            if ((Items == null) != (other.Items == null)) return false;
            if (Items != null &&
                other.Items != null &&
                !Enumerable.SequenceEqual(Items.Select(item => item.MakeExplain()), other.Items.Select(item => item.MakeExplain()))) return false;

            return true;
        }

        public bool Equals(AreaStatusList x, AreaStatusList y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(AreaStatusList obj)
        {
            return obj.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AreaStatusList);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return ZString.Join(",", Items.Select(item => item.MakeExplain()).ToList());
        }

        public async UniTask ReceiveEventAsync(DuelManager duelManager, After after, AreaData areaData, int areaNo)
        {
            foreach (var status in Items.ToList())//項目数が変化する場合があるので複製
            {
                await status.ReceiveEventAsync(duelManager, after, areaData, areaNo);
            }
        }

        static Dictionary<string, IAreaStatus> details = null;

        static Dictionary<string, IAreaStatus> Details
        {
            get
            {
                if (details == null)
                {
                    details = new Dictionary<string, IAreaStatus>()
                    {
                        { nameof(AreaStatusChangeField), new AreaStatusChangeField() },
                        { nameof(AreaStatusAddPlayerCondition), new AreaStatusAddPlayerCondition() },
                        { nameof(AreaStatusAddResource), new AreaStatusAddResource() },
                    };
                }
                return details;
            }
        }

        public static IAreaStatus GetDetail(string innerName) => Details[innerName];

        public static IEnumerable<string> MakeDetailExplains() => Details.Values.Select(pcd =>
        {
            using (var sb = ZString.CreateStringBuilder())
            {
                sb.Append('[');
                sb.Append(pcd.GetNameExample());
                sb.Append(']');
                sb.Append(pcd.GetExplainExample());
                return sb.ToString();
            }
        });
    }
}
