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
    [MemoryPackUnion(0, typeof(AreaStatusAddPlayerCondition))]
    [MemoryPackUnion(1, typeof(AreaStatusAddResource))]
    [MemoryPackUnion(2, typeof(AreaStatusChangeField))]
    public partial interface IAreaStatus
    {
        int RegisteredPlayerNo { get; set; }

        string GetName();

        string MakeExplain();

        bool IsMatch(IAreaStatus other);

        string GetNameExample();

        string GetExplainExample();

        string GetImageName() => string.Empty;

        void Marge(IAreaStatus other);

        IAreaStatus Duplicate();

        UniTask ReceiveEventAsync(DuelManager duelManager, After after, AreaData areaData, int areaNo);
    }
}
