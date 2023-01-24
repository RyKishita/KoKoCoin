using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusBySetCoinName : AddValueStatusBy
    {
        public AddValueStatusBySetCoinName(int value, bool bCoinOwner, string coinName)
            : base(value)
        {
            this.bCoinOwner = bCoinOwner;
            this.coinName = coinName;
        }

        public override string Explain
        {
            get
            {
                int format = (0 < Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    format += 2;
                }
                string coinDisplayName = CoinList.Instance.GetCoin(coinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { "coinname", coinDisplayName },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusBySetCoinName), format, param);
            }
        }

        readonly bool bCoinOwner;
        readonly string coinName;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            return duelEvent is AfterEndUse afterUse &&
                    afterUse.Use.CoinData.CoinName == coinName;
        }
    }
}
