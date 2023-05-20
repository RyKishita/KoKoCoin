using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class DamageToAddConditionTurn : DamageTo
    {
        public DamageToAddConditionTurn(string playerConditionInnerName, int maxCount, Defines.ParticleType particleType)
        {
            this.playerConditionInnerName = playerConditionInnerName;
            this.maxCount = maxCount;
            this.particleType = particleType;
        }

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(Defines.StringEnum.Opponent);
                string conditionname = Duel.PlayerCondition.PlayerConditionDetail.GetLocalizedStringName(playerConditionInnerName);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player},
                    { nameof(conditionname), conditionname},
                    { nameof(maxCount), maxCount.ToString()},
                };

                return GetEffectLocalizedString(nameof(DamageToAddConditionTurn), param);
            }
        }

        readonly string playerConditionInnerName;
        readonly int maxCount;
        readonly Defines.ParticleType particleType;

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterDamageCoin afterDamageCoin)
        {
            if (0 < selectedCoinData.CoinData.Turn)
            {
                var playerNo = afterDamageCoin.TakePlayerNo;
                var pc = new Duel.PlayerCondition.PlayerCondition(playerConditionInnerName, Math.Min(selectedCoinData.CoinData.Turn, maxCount));

                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = playerNo,
                    ParticleType = particleType
                });

                duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
                {
                    PlayerNo = playerNo,
                    PlayerCondition = pc
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
