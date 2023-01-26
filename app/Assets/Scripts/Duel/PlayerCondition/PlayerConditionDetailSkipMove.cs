using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailSkipMove : PlayerConditionDetail, IPlayerConditionInterceptDuelStep
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailSkipMove));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.Gravity;

        readonly Defines.DuelPhase playerTurnStep = Defines.DuelPhase.移動;

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
            return GetLocalizedString(nameof(PlayerConditionDetailSkipMove), new Dictionary<string, string>
            {
                { nameof(count), count }
            });
        }

        public bool IsInterceptDuelStep(DuelData duelData, int targetPlayerNo, Player player, Defines.DuelPhase gamePhase)
        {
            return playerTurnStep == gamePhase &&
                    targetPlayerNo == player.PlayerNo &&
                    player.ConditionList.IsEffectStatus<PlayerConditionDetailSkipMove>(duelData);
        }

        public UniTask InterceptDuelStep(DuelManager duelManager, Player player)
        {
            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = player.PlayerNo,
                ParticleType = Defines.ParticleType.SkipDuelStep,
            });

            duelManager.RegistDuelEventAction(new ActionRemovePlayerCondition()
            {
                PlayerNo = player.PlayerNo,
                PlayerConditionName = nameof(PlayerConditionDetailSkipMove)
            });

            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailSkipMove), value);
        }
    }
}
