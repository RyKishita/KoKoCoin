using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailSkipDraw : PlayerConditionDetail, IPlayerConditionInterceptDuelStep
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailSkipDraw));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.Anesthetize;

        readonly Defines.DuelPhase playerTurnStep = Defines.DuelPhase.コイン取得;

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
            return GetLocalizedString(nameof(PlayerConditionDetailSkipDraw), new Dictionary<string, string>
            {
                { nameof(count), count }
            });
        }

        public bool IsInterceptDuelStep(DuelData duelData, int targetPlayerNo, Player player, Defines.DuelPhase gamePhase)
        {
            return playerTurnStep == gamePhase &&
                    targetPlayerNo == player.PlayerNo &&
                    player.ConditionList.IsEffectStatus<PlayerConditionDetailSkipDraw>(duelData);
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
                PlayerConditionName = nameof(PlayerConditionDetailSkipDraw)
            });

            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailSkipDraw), value);
        }
    }
}
