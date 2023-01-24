using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByMoveCoinAnyFrom : AddValueStatusBy
    {
        public AddValueStatusByMoveCoinAnyFrom(
            int value,
            bool bCoinOwner)
            : base(value)
        {
            this.bCoinOwner = bCoinOwner;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    formatType += 2;
                }
                var param = new Dictionary<string, string>()
                {
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByMoveCoinAnyFrom), formatType, param);
            }
        }

        readonly bool bCoinOwner;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            var owner = selectedCoinData.CoinData.OwnerPlayerNo;

            if (after is AfterMoveCoinsToSet afterMoveCoinsToSet)
            {
                return afterMoveCoinsToSet.SrcItems
                    .Where(item => item.CoinPosition == Defines.CoinPosition.Field)
                    .Where(item => duelData.GetCoin(item.CoinID).OwnerPlayerNo == owner)
                    .Any();
            }
            else if (after is AfterMoveCoinsToPlayer afterMoveCoinsToPlayer)
            {
                return  afterMoveCoinsToPlayer.SrcItems
                    .Where(item => item.CoinPosition == Defines.CoinPosition.Field)
                    .Where(item => duelData.GetCoin(item.CoinID).OwnerPlayerNo == owner)
                    .Any();
            }
            return false;
        }
    }
}
