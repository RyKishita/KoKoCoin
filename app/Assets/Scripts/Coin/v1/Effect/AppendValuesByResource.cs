using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValuesByResource : AppendValuesByCount
    {
        public AppendValuesByResource(bool bCoinOwner, int value)
            : base(value)
        {
            this.bCoinOwner = bCoinOwner;
        }

        public override string Explain
        {
            get
            {
                int format = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string resource = Defines.GetLocalizedString(Defines.StringEnum.Resource);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(resource), resource },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValuesByResource), format, param);
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
