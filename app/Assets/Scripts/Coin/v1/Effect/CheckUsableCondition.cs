using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Assets.Scripts.Duel.PlayerCondition;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableCondition : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableCondition(bool bCoinOwner, PlayerCondition playerCondition)
        {
            if (playerCondition.Value <= 0) throw new ArgumentException("状態異常値は1以上");

            this.bCoinOwner = bCoinOwner;
            this.playerCondition = playerCondition;
        }

        readonly bool bCoinOwner;
        readonly PlayerCondition playerCondition;

        public override string Explain
        {
            get
            {
                int format = bCoinOwner ? 0 : 1;
                string conditionname = playerCondition.GetDisplayName();
                string num = Math.Abs(playerCondition.Value).ToString();

                var param = new Dictionary<string, string>()
                {
                    { nameof(conditionname), conditionname },
                    { nameof(num), num },
                };

                return GetEffectLocalizedString(nameof(CheckUsableCondition), format, param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                var playerNos = bCoinOwner
                                ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                                : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
                return playerNos.Any(playerNo => duelData.Players[playerNo].ConditionList.Has(playerCondition));
            }
            return true;
        }

        public override bool IsProcessedOnArea()
        {
            return false;
        }
    }
}
