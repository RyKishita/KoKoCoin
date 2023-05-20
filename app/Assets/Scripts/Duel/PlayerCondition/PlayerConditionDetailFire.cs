using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Duel.PlayerCondition
{
    /// <summary>
    /// ターン毎カウント×ダメージ
    /// ターン毎カウント 半減
    /// </summary>
    class PlayerConditionDetailFire : PlayerConditionDetail, IPlayerConditionTriggerEvent
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailFire));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.Fire;

        public override string MakeExplain()
        {
            return MakeExplain(
                Defines.GetLocalizedString(Defines.StringEnum.ConditionBaseDamage));
        }

        public override string MakeExplain(DuelData duelData, PlayerCondition playerCondition)
        {
            return MakeExplain(
                duelData.GetBaseStatusDamage(playerCondition.InnerName).ToString());
        }

        string MakeExplain(string value)
        {
            var param = new Dictionary<string, string>
            {
                { nameof(value), value }
            };

            return GetLocalizedString(nameof(PlayerConditionDetailFire), param);
        }

        public bool IsReceiveEvent(DuelData duelData, Player player, After duelEvent)
        {
            return duelEvent is AfterStep afterStep &&
                    afterStep.GamePhase == Defines.DuelPhase.ターン終了 &&
                    player.ConditionList.Has<PlayerConditionDetailFire>();
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, Player player, After duelEvent)
        {
            if (IsReceiveEvent(duelManager.DuelData, player, duelEvent))
            {
                duelManager.FocusPlayer(player.PlayerNo);

                var baseDamage = duelManager.DuelData.GetBaseStatusDamage(nameof(PlayerConditionDetailFire));

                var pc = player.ConditionList.GetItem<PlayerConditionDetailFire>();

                duelManager.RegistDuelEventAction(new ActionDamageStatus()
                {
                    TakePlayerNo = player.PlayerNo,
                    Damage = baseDamage * pc.Value,
                    PlayerConditionName = nameof(PlayerConditionDetailFire),
                    ParticleType = Defines.ParticleType.Fire,
                });

                duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
                {
                    PlayerNo = player.PlayerNo,
                    PlayerCondition = CreatePlayerCondition(pc.Value / 2 - pc.Value)
                });
            }

            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailFire), value);
        }
    }
}
