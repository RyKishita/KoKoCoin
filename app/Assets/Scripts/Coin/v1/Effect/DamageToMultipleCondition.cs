using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class DamageToMultipleCondition : DamageTo
    {
        public DamageToMultipleCondition(bool bCoinOwner, string playerConditionInnerName, int value, Defines.ParticleType particleType)
        {
            if (value < 2) throw new ArgumentOutOfRangeException(nameof(value));
            this.bCoinOwner = bCoinOwner;
            this.playerConditionInnerName = playerConditionInnerName;
            this.value = value;
            this.particleType = particleType;
        }

        public override string Explain
        {
            get
            {
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string conditionname = Duel.PlayerCondition.PlayerConditionDetail.GetLocalizedStringName(playerConditionInnerName);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(conditionname), conditionname },
                    { nameof(value), value.ToString() },
                };

                return GetEffectLocalizedString(nameof(DamageToMultipleCondition), param);
            }
        }

        readonly bool bCoinOwner;
        readonly string playerConditionInnerName;
        readonly int value;
        readonly Defines.ParticleType particleType;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return base.IsReceiveEvent(duelData, selectedCoinData, duelEvent) &&
                    duelEvent is AfterDamageCoin duelEventAfterDamage &&
                    duelData.Players[duelEventAfterDamage.TakePlayerNo].ConditionList.Has(playerConditionInnerName);
        }

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterDamageCoin afterDamageCoin)
        {
            var playerNo = afterDamageCoin.TakePlayerNo;
            var pc = duelManager.DuelData.Players[playerNo].ConditionList.GetItem(playerConditionInnerName);
            if (pc != null)
            {
                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = playerNo,
                    ParticleType = particleType
                });

                duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
                {
                    PlayerNo = playerNo,
                    PlayerCondition = new Duel.PlayerCondition.PlayerCondition(playerConditionInnerName, pc.Value * (value - 1)),
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
