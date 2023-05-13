using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailSkipDirectAttack : PlayerConditionDetail, IPlayerConditionInterceptSelectCoin, IPlayerCondtionCount
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
            return MakeExplain(duelData.GetConditionCount(playerCondition.InnerName).ToString());
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

        public bool InterceptSelectCoin(DuelManager duelManager, int targetPlayerNo, Defines.CoinType selectCoinType, Player player)
        {
            if (!selectCoinType.HasFlag(coinType)) return false;
            if (targetPlayerNo != player.PlayerNo) return false;
            if (!player.ConditionList.IsEffectStatus<PlayerConditionDetailSkipDirectAttack>(duelManager.DuelData)) return false;

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

            return true;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailSkipDirectAttack), value);
        }
    }
}
