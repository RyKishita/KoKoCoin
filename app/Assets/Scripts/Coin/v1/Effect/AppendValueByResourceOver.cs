using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByResourceOver : AppendValueByCount
    {
        public AppendValueByResourceOver(bool bCoinOwner, int num, int value)
            : base(num, value)
        {
            if (num <= 0) throw new ArgumentOutOfRangeException(nameof(num));
            this.bCoinOwner = bCoinOwner;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string resource = Defines.GetLocalizedString(Defines.StringEnum.Resource);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(resource), resource },
                    { nameof(num), num.ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByResourceOver), formatType, param);
            }
        }

        readonly bool bCoinOwner;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            return bCoinOwner
                    ? duelData.Players[playerNo].TurnResource
                    : duelData.GetOtherTeamPlayers(playerNo).Sum(otherPlayer => otherPlayer.TurnResource);
        }
    }
}
