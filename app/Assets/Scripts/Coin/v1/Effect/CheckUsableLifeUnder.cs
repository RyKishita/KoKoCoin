using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableLifeUnder : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableLifeUnder(bool bCoinOwner, int life)
        {
            if (life <= 0) throw new ArgumentOutOfRangeException(nameof(life));
            this.bCoinOwner = bCoinOwner;
            this.life = life;
        }

        readonly bool bCoinOwner;
        readonly int life;

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { "value", life.ToString() },
                };

                return GetEffectLocalizedString(nameof(CheckUsableLifeUnder), param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                var playerNos = bCoinOwner
                                ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                                : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
                return playerNos.Any(playerNo => duelData.Players[playerNo].Life <= life);
            }

            return true;
        }

        public override bool IsProcessedOnArea()
        {
            return false;
        }
    }
}
