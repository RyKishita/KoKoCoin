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
    class AddConditionToPlayerBySet : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public AddConditionToPlayerBySet(PlayerCondition playerCondition, Defines.ParticleType particleType)
        {
            this.playerCondition = playerCondition;
            this.particleType = particleType;
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

                return GetEffectLocalizedString(nameof(AddConditionToPlayerBySet), param);
            }
        }

        readonly PlayerCondition playerCondition;
        readonly Defines.ParticleType particleType;

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
                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = selectedCoinData.CoinData.OwnerPlayerNo,
                    ParticleType = particleType
                });

                duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
                {
                    PlayerNo = selectedCoinData.CoinData.OwnerPlayerNo,
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
