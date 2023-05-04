using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using MemoryPack;

namespace Assets.Scripts.Duel.CoinStatus
{
    [MemoryPackable]
    public partial class CoinStatusList : ICloneable, IEquatable<CoinStatusList>, IEqualityComparer<CoinStatusList>
    {
        [MemoryPackConstructor]
        public CoinStatusList()
        {

        }

        public CoinStatusList(CoinStatusList src)
        {
            Items = src.Items.Select(item => item.Duplicate()).ToList();
        }

        public List<ICoinStatus> Items = new List<ICoinStatus>();

        public void Regist(ICoinStatus status)
        {
            var findItem = Items.FirstOrDefault(item => status.IsMatch(item));
            if (null == findItem)
            {
                Items.Add(status.Duplicate());
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

        public bool Clear()
        {
            if (IsEmpty()) return false;
            Items.Clear();
            return true;
        }

        public bool RemoveBy<T>() where T : ICoinStatus
        {
            return 0 < Items.RemoveAll(item => item is T);
        }

        public bool RemoveBy(ICoinStatus status)
        {
            return 0 < Items.RemoveAll(item => status.IsMatch(item));
        }

        public T GetItem<T>() where T : ICoinStatus
        {
            return Items.Where(item => item is T).Cast<T>().FirstOrDefault();
        }

        public ICoinStatus GetItem(Type type)
        {
            return Items.Where(item => item.GetType() == type).FirstOrDefault();
        }

        public bool Has<T>() where T : ICoinStatus
        {
            return Items.Any(item => item is T);
        }

        public bool Has(ICoinStatus status)
        {
            return Items.Any(item => status.IsMatch(item));
        }

        public IEnumerable<string> GetExplains()
        {
            foreach (var item in Items)
            {
                yield return item.GetExplain();
            }
        }

        public IEnumerable<string> GetNames()
        {
            foreach (var item in Items)
            {
                yield return item.GetName();
            }
        }

        public string GetName() => ZString.Join(" ", GetNames().ToList());

        public object Clone()
        {
            return Duplicate();
        }

        public CoinStatusList Duplicate()
        {
            return new CoinStatusList(this);
        }

        public bool Equals(CoinStatusList other)
        {
            if (other == null) return false;
            if (ReferenceEquals(other, this)) return true;

            if (Items.Count != other.Items.Count) return false;
            if (!Items.All(item => other.Items.Any(item2 => item.GetName() == item2.GetName()))) return false;

            return true;
        }

        public bool Equals(CoinStatusList x, CoinStatusList y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(CoinStatusList obj)
        {
            return obj.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CoinStatusList);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return ZString.Join(",", Items.Select(item => item.GetName()).ToList());
        }

        public async UniTask ReceiveEvent(DuelManager duelManager, CoinData coinData, DuelEvent.After duelEvent)
        {
            foreach(var item in Items.ToList())//項目数が変化する場合があるので複製
            {
                await item.ReceiveEvent(duelManager, coinData, duelEvent);
            }
        }

        public void ReceiveEvent<T>(DuelManager duelManager, CoinData coinData, DuelEvent.After duelEvent) where T : ICoinStatus
        {
            GetItem<T>()?.ReceiveEvent(duelManager, coinData, duelEvent);
        }

        static Dictionary<string, ICoinStatus> details = null;

        static Dictionary<string, ICoinStatus> Details
        {
            get
            {
                if (details == null)
                {
                    details = new Dictionary<string, ICoinStatus>()
                    {
                        { nameof(CoinStatusAppendValue), new CoinStatusAppendValue() },
                        { nameof(CoinStatusInvalidEffectByTurn), new CoinStatusInvalidEffectByTurn() },

                        // 与えるコインが無い
                        //{ nameof(CoinStatusAppendTag), new CoinStatusAppendTag() },
                        //{ nameof(CoinStatusTurnToTrash), new CoinStatusTurnToTrash() },
                    };
                }
                return details;
            }
        }

        public static ICoinStatus GetDetail(string innerName) => Details[innerName];

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
