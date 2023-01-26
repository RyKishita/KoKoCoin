using System;
using System.Collections.Generic;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using MemoryPack;

namespace Assets.Scripts.Duel.CoinStatus
{
    [MemoryPackable]
    [MemoryPackUnion(0, typeof(CoinStatusAppendTag))]
    [MemoryPackUnion(1, typeof(CoinStatusAppendValue))]
    [MemoryPackUnion(2, typeof(CoinStatusInvalidEffectByTurn))]
    [MemoryPackUnion(3, typeof(CoinStatusTurnToTrash))]
    public partial interface ICoinStatus
    {
        /// <summary>
        /// ステータスを付与した人。主に自動操作で使用
        /// </summary>
        int RegisteredPlayerNo { get; set; }

        string GetName();

        string GetExplain();

        string GetNameExample();

        string GetExplainExample();

        ICoinStatus Duplicate();

        bool IsMatch(ICoinStatus other);

        void Marge(ICoinStatus other);

        UniTask ReceiveEvent(DuelManager duelManager, CoinData coinData, After duelEvent);
    }
}
