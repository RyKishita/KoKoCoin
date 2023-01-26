using System;
using System.Collections.Generic;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using MemoryPack;

namespace Assets.Scripts.Duel.CoinStatus
{
    /// <summary>
    /// タグ付与
    /// </summary>
    [MemoryPackable]
    public partial class CoinStatusAppendTag : ICoinStatus, ICloneable
    {
        [MemoryPackConstructor]
        public CoinStatusAppendTag()
        {

        }

        public CoinStatusAppendTag(CoinStatusAppendTag src)
        {
            RegisteredPlayerNo = src.RegisteredPlayerNo;
            CoinTag = src.CoinTag;
        }

        #region serialize

        public int RegisteredPlayerNo { get; set; }

        /// <summary>
        /// 値
        /// </summary>
        public Defines.CoinTag CoinTag;

        #endregion

        public string GetName() => MakeName(CoinTag.ToString("G"));

        public string GetExplain() => MakeExplain(CoinTag.ToString("G"));

        public string GetNameExample() => MakeName("?");

        public string GetExplainExample() => MakeExplain("?");

        public object Clone()
        {
            return Duplicate();
        }

        public ICoinStatus Duplicate()
        {
            return new CoinStatusAppendTag(this);
        }

        public bool IsMatch(ICoinStatus other)
        {
            return other is CoinStatusAppendTag;
        }

        public void Marge(ICoinStatus other)
        {
            RegisteredPlayerNo = other.RegisteredPlayerNo;
            CoinTag |= (other as CoinStatusAppendTag).CoinTag;
        }

        string MakeName(string value)
        {
            var param = new Dictionary<string, string>()
            {
                { nameof(value), value },
            };
            return Functions.GetLocalizedStringName(nameof(CoinStatusAppendTag), param);
        }

        string MakeExplain(string value)
        {
            var param = new Dictionary<string, string>()
            {
                { nameof(value), value },
            };
            return Functions.GetLocalizedString(nameof(CoinStatusAppendTag), param);
        }

        public UniTask ReceiveEvent(DuelManager duelManager, CoinData coinData, After duelEvent)
        {
            return UniTask.CompletedTask;
        }
    }
}
