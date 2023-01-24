using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByResourceOver : AddValueStatusBy
    {
        public AddValueStatusByResourceOver(int value, bool bCoinOwner, int num)
            : base(value)
        {
            if (num <= 0) throw new ArgumentOutOfRangeException(nameof(num));
            this.bCoinOwner = bCoinOwner;
            this.num = num;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string resource = Defines.GetLocalizedString(Defines.StringEnum.Resource);

                var param = new Dictionary<string, string>()
                {
                    { nameof(field), field },
                    { nameof(player), player },
                    { nameof(resource), resource },
                    { nameof(num), num.ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByResourceOver), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly int num;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            var playerNos = bCoinOwner
                            ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                            : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
            switch (duelEvent)
            {
                case AfterChangeResource afterChangeResource:
                    return playerNos.Contains(afterChangeResource.PlayerNo) &&
                            num <= afterChangeResource.NewResource;
                case AfterAddResource afterAddResource:
                    return playerNos.Contains(afterAddResource.PlayerNo) &&
                            num <= afterAddResource.NewResource;
            }
            return false;
        }
    }
}
