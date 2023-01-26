using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Duel.PlayerCondition
{
    /// <summary>
    /// 規定値を超えると行動がランダムになる
    /// </summary>
    class PlayerConditionDetailConfusionMove : PlayerConditionDetail, IPlayerConditionInterceptMoveWorst
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailConfusionMove));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.ConfusionMove;

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
            return GetLocalizedString(nameof(PlayerConditionDetailConfusionMove), new Dictionary<string, string>
            {
                { nameof(count), count }
            });
        }

        public bool IsInterceptMoveWorst(DuelData duelData, int targetPlayerNo, Player player)
        {
            return targetPlayerNo == player.PlayerNo &&
                player.ConditionList.IsEffectStatus<PlayerConditionDetailConfusionMove>(duelData);
        }

        public UniTask InterceptMoveWorst(DuelManager duelManager, Player player)
        {
            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = player.PlayerNo,
                ParticleType = Defines.ParticleType.ConfusionMove
            });

            duelManager.RegistDuelEventAction(new ActionRemovePlayerCondition()
            {
                PlayerNo = player.PlayerNo,
                PlayerConditionName = nameof(PlayerConditionDetailConfusionMove)
            });

            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailConfusionMove), value);
        }
    }
}
