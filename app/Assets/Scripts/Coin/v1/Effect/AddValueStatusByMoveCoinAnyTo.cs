using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByMoveCoinAnyTo : AddValueStatusBy
    {
        public AddValueStatusByMoveCoinAnyTo(
            int value,
            bool bCoinOwner,
            Defines.CoinPosition dstCoinPosition)
            : base(value)
        {
            if (dstCoinPosition == Defines.CoinPosition.None) throw new ArgumentOutOfRangeException(nameof(dstCoinPosition));
            this.bCoinOwner = bCoinOwner;
            this.dstCoinPosition = dstCoinPosition;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);
                string dst = Defines.GetLocalizedString(dstCoinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(field), field },
                    { "value", Math.Abs(Value).ToString() },
                    { nameof(dst), dst },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByMoveCoinAnyTo), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition dstCoinPosition;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            var owner = selectedCoinData.CoinData.OwnerPlayerNo;

            if (dstCoinPosition == Defines.CoinPosition.Field)
            {
                return after is AfterMoveCoinsToSet afterMoveCoinsToSet &&
                    afterMoveCoinsToSet.SrcItems
                        .Where(item => duelData.GetCoin(item.CoinID).OwnerPlayerNo == owner)
                        .Any();
            }
            else
            {
                return after is AfterMoveCoinsToPlayer afterMoveCoinsToPlayer &&
                        afterMoveCoinsToPlayer.DstCoinPosition == dstCoinPosition &&
                        afterMoveCoinsToPlayer.SrcItems
                            .Where(item => duelData.GetCoin(item.CoinID).OwnerPlayerNo == owner)
                            .Any();
            }
        }
    }
}
