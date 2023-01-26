using System;
using System.Collections.Generic;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using MemoryPack;

namespace Assets.Scripts.Duel.CoinStatus
{
    /// <summary>
    /// 効果補正。マイナスの場合もある
    /// </summary>
    [MemoryPackable]
    public partial class CoinStatusAppendValue : ICoinStatus, ICloneable
    {
        [MemoryPackConstructor]
        public CoinStatusAppendValue()
        {

        }

        public CoinStatusAppendValue(CoinStatusAppendValue src)
        {
            RegisteredPlayerNo = src.RegisteredPlayerNo;
            Value = src.Value;
        }

        #region serialize

        public int RegisteredPlayerNo { get; set; }

        /// <summary>
        /// 値
        /// </summary>
        public int Value;

        #endregion

        public string GetName()
        {
            int format = (0 < Value) ? 0 : 1;
            var value = Math.Abs(Value).ToString();

            var param = new Dictionary<string, string>()
            {
                { nameof(value), value },
            };
            return Functions.GetLocalizedStringName(nameof(CoinStatusAppendValue), format, param);
        }

        public string GetExplain()
        {
            int format = (0 < Value) ? 0 : 1;
            var value = Math.Abs(Value).ToString();

            var param = new Dictionary<string, string>()
            {
                { nameof(value), value },
            };
            return Functions.GetLocalizedString(nameof(CoinStatusAppendValue), format, param);
        }

        public string GetNameExample() => Functions.GetLocalizedStringName(nameof(CoinStatusAppendValue));

        public string GetExplainExample() => Functions.GetLocalizedString(nameof(CoinStatusAppendValue));

        public object Clone()
        {
            return Duplicate();
        }

        public ICoinStatus Duplicate()
        {
            return new CoinStatusAppendValue(this);
        }

        public bool IsMatch(ICoinStatus other)
        {
            return other is CoinStatusAppendValue;
        }

        public void Marge(ICoinStatus other)
        {
            RegisteredPlayerNo = other.RegisteredPlayerNo;
            Value += (other as CoinStatusAppendValue).Value;
        }

        public UniTask ReceiveEvent(DuelManager duelManager, CoinData coinData, After duelEvent)
        {
            return UniTask.CompletedTask;
        }
    }
}
