using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Assets.Scripts.Duel.PlayerCondition;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddConditionToAreaBySet : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public AddConditionToAreaBySet(PlayerCondition playerCondition)
        {
            this.playerCondition = playerCondition;
        }

        public override string Explain
        {
            get
            {
                string conditionname = playerCondition.GetDisplayName();

                var param = new Dictionary<string, string>()
                {
                    { nameof(conditionname), conditionname},
                    { "value", playerCondition.Value.ToString()},
                };

                return GetEffectLocalizedString(nameof(AddConditionToAreaBySet), param);
            }
        }

        readonly PlayerCondition playerCondition;

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return after is AfterMoveCoinsToSet afterMoveCoinsToSet &&
                afterMoveCoinsToSet.CoinMoveReason == Defines.CoinMoveReason.Set &&
                afterMoveCoinsToSet.SrcItems.Any(item => item.CoinID == selectedCoinData.CoinData.ID);
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                var afterMoveCoinsToSet = after as AfterMoveCoinsToSet;

                duelManager.RegistDuelEventAction(new ActionAddConditionToArea()
                {
                    ExecutePlayerNo = selectedCoinData.CoinData.OwnerPlayerNo,
                    AreaNo = afterMoveCoinsToSet.DstAreaNo,
                    PlayerCondition = playerCondition
                });
            }

            return UniTask.CompletedTask;
        }

        public override bool IsProcessedOnArea()
        {
            return false;
        }
    }
}
