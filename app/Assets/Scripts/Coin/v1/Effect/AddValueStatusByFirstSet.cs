using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByFirstSet : AddValueStatusBy
    {
        public AddValueStatusByFirstSet(int value)
            : base(value)
        {
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);

                var param = new Dictionary<string, string>()
                {
                    { nameof(field), field },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByFirstSet), formatType, param);
            }
        }

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            return duelEvent is AfterMoveCoinsToSet afterMoveCoinsToSet &&
                    afterMoveCoinsToSet.CoinMoveReason == Defines.CoinMoveReason.Set &&
                    afterMoveCoinsToSet.SrcItems.All(item => item.CoinID == selectedCoinData.CoinData.ID) &&
                    duelData.FieldData.GetAllAreaCoins().All(scd =>
                        scd.CoinData.OwnerPlayerNo != selectedCoinData.CoinData.OwnerPlayerNo ||
                        scd.CoinData.ID == selectedCoinData.CoinData.ID);
        }

        protected override bool IsMatchAddValueStatus(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            return duelData.FieldData.GetAllAreaCoins().All(scd => scd.CoinData.OwnerPlayerNo != selectedCoinData.CoinData.OwnerPlayerNo);
        }
    }
}
