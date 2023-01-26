using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Duel.PlayerCondition
{
    /// <summary>
    /// 付与時に固定ダメージ
    /// 自身にも同数を付与。ダメージは無し
    /// 貯めすぎると小ダメージを受けてカウント0になる
    /// </summary>
    class PlayerConditionDetailElectric : PlayerConditionDetail, IPlayerConditionTriggerEvent
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailElectric));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.Electric;

        public override string MakeExplain()
        {
            return MakeExplain(
                Defines.GetLocalizedString(Defines.StringEnum.ConditionCount),
                Defines.GetLocalizedString(Defines.StringEnum.ConditionBaseDamage));
        }

        public override string MakeExplain(DuelData duelData, PlayerCondition playerCondition)
        {
            return MakeExplain(
                duelData.GetConditionCount().ToString(),
                duelData.GetBaseStatusDamage(playerCondition.InnerName).ToString());
        }

        string MakeExplain(string count, string value)
        {
            var param = new Dictionary<string, string>
            {
                { nameof(count), count },
                { nameof(value), value }
            };

            return GetLocalizedString(nameof(PlayerConditionDetailElectric), param);
        }

        public bool IsReceiveEvent(DuelData duelData, Player player, After duelEvent)
        {
            return duelEvent is AfterStep afterStep &&
                    afterStep.GamePhase == Defines.DuelPhase.ターン終了 &&
                    player.ConditionList.IsEffectStatus<PlayerConditionDetailElectric>(duelData);
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, Player player, After duelEvent)
        {
            if (IsReceiveEvent(duelManager.DuelData, player, duelEvent))
            {
                duelManager.FocusPlayer(player.PlayerNo);

                var baseDamage = duelManager.DuelData.GetBaseStatusDamage(nameof(PlayerConditionDetailElectric));

                duelManager.RegistDuelEventAction(new ActionDamageStatus()
                {
                    DiffencePlayerNo = player.PlayerNo,
                    Damage = baseDamage,
                    ParticleType = Defines.ParticleType.Electric,
                });

                duelManager.RegistDuelEventAction(new ActionRemovePlayerCondition()
                {
                    PlayerNo = player.PlayerNo,
                    PlayerConditionName = nameof(PlayerConditionDetailElectric)
                });
            }
            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailElectric), value);
        }
    }
}
