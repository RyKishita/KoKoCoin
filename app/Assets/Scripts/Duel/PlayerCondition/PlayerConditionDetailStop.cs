using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Duel.PlayerCondition
{
    /// <summary>
    /// 規定値を超えると一回やすみとなり、カウントはゼロに
    /// </summary>
    class PlayerConditionDetailStop : PlayerConditionDetail, IPlayerConditionInterceptDuelStep
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailStop));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.SkipDuelStep;

        readonly Defines.DuelPhase playerTurnStep = Defines.DuelPhase.ターン開始;

        public override string MakeExplain()
        {
            return MakeExplain(Defines.GetLocalizedString(Defines.StringEnum.ConditionCount));
        }

        public override string MakeExplain(DuelData duelData, PlayerCondition playerCondition)
        {
            return MakeExplain(duelData.GetConditionCount().ToString());
        }

        string MakeExplain(string count)
        {
            return GetLocalizedString(nameof(PlayerConditionDetailStop), new Dictionary<string, string>
            {
                { nameof(count), count }
            });
        }

        public bool IsInterceptDuelStep(DuelData duelData, int targetPlayerNo, Player player, Defines.DuelPhase gamePhase)
        {
            return playerTurnStep == gamePhase &&
                    targetPlayerNo == player.PlayerNo &&
                    player.ConditionList.IsEffectStatus<PlayerConditionDetailStop>(duelData);
        }

        public UniTask InterceptDuelStep(DuelManager duelManager, Player player)
        {
            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = player.PlayerNo,
                ParticleType = Defines.ParticleType.Gravity,
            });

            duelManager.RegistDuelEventAction(new ActionRemovePlayerCondition()
            {
                PlayerNo = player.PlayerNo,
                PlayerConditionName = nameof(PlayerConditionDetailStop)
            });

            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailStop), value);
        }
    }
}
