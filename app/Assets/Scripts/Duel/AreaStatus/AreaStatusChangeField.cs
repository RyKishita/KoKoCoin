using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using MemoryPack;

namespace Assets.Scripts.Duel.AreaStatus
{
    [MemoryPackable]
    public partial class AreaStatusChangeField : IAreaStatus
    {
        [MemoryPackConstructor]
        public AreaStatusChangeField()
        {

        }
        public AreaStatusChangeField(AreaStatusChangeField src)
        {
            RegisteredPlayerNo = src.RegisteredPlayerNo;
            AreaType = src.AreaType;
        }

        #region serialize

        public int RegisteredPlayerNo { get; set; }

        public Defines.AreaType AreaType;

        #endregion

        public string GetName() => MakeName(AreaType.ToString());

        public string MakeExplain()
        {
            return MakeExplain(AreaType.ToString());
        }

        public bool IsMatch(IAreaStatus other)
        {
            return other is AreaStatusChangeField;
        }

        public string GetNameExample() => MakeName("?");

        string MakeName(string value)
        {
            var param = new Dictionary<string, string>()
            {
                { nameof(value), value }
            };
            return Functions.GetLocalizedStringName(nameof(AreaStatusChangeField), param);
        }

        public string GetExplainExample() => MakeExplain("?");

        string MakeExplain(string value)
        {
            var param = new Dictionary<string, string>()
            {
                { nameof(value), value }
            };
            return Functions.GetLocalizedString(nameof(AreaStatusChangeField), param);
        }

        public void Marge(IAreaStatus other)
        {
            RegisteredPlayerNo = other.RegisteredPlayerNo;
            AreaType = (other as AreaStatusChangeField).AreaType;
        }

        public IAreaStatus Duplicate()
        {
            return new AreaStatusChangeField(this);
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, After after, AreaData areaData, int areaNo)
        {
            return UniTask.CompletedTask;
        }
    }
}
