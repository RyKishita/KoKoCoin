using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailSkipDirectAttack : PlayerConditionDetail, IPlayerConditionInterceptSelectCoin
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailSkipDirectAttack));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.SkipDuelStep;

        readonly Defines.CoinType coinType = Defines.CoinType.DirectAttack;

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
            var directattackcoin = Defines.GetLocalizedString(Defines.StringEnum.DirectAttackCoin);

            return GetLocalizedString(nameof(PlayerConditionDetailSkipDirectAttack), new Dictionary<string, string>
            {
                { nameof(directattackcoin), directattackcoin},
                { nameof(count), count }
            });
        }

        public bool IsInvalidSelectCoinType(DuelData duelData, int targetPlayerNo, Player player, Defines.CoinType selectCoinType)
        {
            return selectCoinType.HasFlag(coinType) &&
                targetPlayerNo == player.PlayerNo &&
                player.ConditionList.IsEffectStatus<PlayerConditionDetailSkipDirectAttack>(duelData);
        }

        public UniTask InterceptSelectCoin(DuelManager duelManager, Player player)
        {
            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = player.PlayerNo,
                ParticleType = Defines.ParticleType.SkipDuelStep,
            });

            duelManager.RegistDuelEventAction(new ActionRemovePlayerCondition()
            {
                PlayerNo = player.PlayerNo,
                PlayerConditionName = nameof(PlayerConditionDetailSkipDirectAttack)
            });

            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailSkipDirectAttack), value);
        }
    }
}
