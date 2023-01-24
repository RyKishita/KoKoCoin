using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByChangeVisibleSide : AddValueStatusBy
    {
        public AddValueStatusByChangeVisibleSide(
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
                int format = (0 < Value) ? 0 : 1;

                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);
                string settedcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);

                var param = new Dictionary<string, string>()
                {
                    { nameof(field), field },
                    { nameof(settedcoin), settedcoin },
                    { nameof(player), player },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByChangeVisibleSide), format, param);
            }
        }

        readonly bool bCoinOwner;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            var playerNos = bCoinOwner
                            ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                            : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
            return duelEvent is AfterChangeReverse afterChangeReverse &&
                    playerNos.Contains(afterChangeReverse.TargetCoin.CoinData.OwnerPlayerNo) &&
                    !afterChangeReverse.IsReverseDst;
        }
    }
}
