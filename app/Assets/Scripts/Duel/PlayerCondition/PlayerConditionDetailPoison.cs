using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Duel.PlayerCondition
{
    /// <summary>
    /// ターン毎固定ダメージ
    /// ターン毎カウント 1減少
    /// 既定カウント以上ならダメージとカウント減少をまとめて処理
    /// </summary>
    class PlayerConditionDetailPoison : PlayerConditionDetail, IPlayerConditionTriggerEvent, IPlayerCondtionCount
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailPoison));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.Poison;

        public override string MakeExplain()
        {
            return MakeExplain(
                Defines.GetLocalizedString(Defines.StringEnum.ConditionCount),
                Defines.GetLocalizedString(Defines.StringEnum.ConditionBaseDamage));
        }

        public override string MakeExplain(DuelData duelData, PlayerCondition playerCondition)
        {
            return MakeExplain(
                duelData.GetConditionCount(playerCondition.InnerName).ToString(),
                duelData.GetBaseStatusDamage(playerCondition.InnerName).ToString());
        }

        string MakeExplain(string count, string value)
        {
            var param = new Dictionary<string, string>
            {
                { nameof(count), count },
                { nameof(value), value }
            };

            return GetLocalizedString(nameof(PlayerConditionDetailPoison), param);
        }

        public bool IsReceiveEvent(DuelData duelData, Player player, After duelEvent)
        {
            return duelEvent is AfterStep afterStep &&
                    afterStep.GamePhase == Defines.DuelPhase.ターン終了 &&
                    player.ConditionList.Has<PlayerConditionDetailPoison>();
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, Player player, After duelEvent)
        {
            if (IsReceiveEvent(duelManager.DuelData, player, duelEvent))
            {
                duelManager.FocusPlayer(player.PlayerNo);

                var pc = player.ConditionList.GetItem<PlayerConditionDetailPoison>();

                int damage = duelManager.DuelData.GetBaseStatusDamage(nameof(PlayerConditionDetailPoison));
                int count = -1;
                if (player.ConditionList.IsEffectStatus<PlayerConditionDetailPoison>(duelManager.DuelData))
                {
                    damage *= pc.Value;
                    count *= pc.Value;
                }

                duelManager.RegistDuelEventAction(new ActionDamageStatus()
                {
                    TakePlayerNo = player.PlayerNo,
                    Damage = damage,
                    PlayerConditionName = nameof(PlayerConditionDetailPoison),
                    ParticleType = Defines.ParticleType.Poison,
                });

                duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
                {
                    PlayerNo = player.PlayerNo,
                    PlayerCondition = CreatePlayerCondition(count)
                });
            }
            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailPoison), value);
        }
    }
}
