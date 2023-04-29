using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailSkipGuard : PlayerConditionDetail, IPlayerConditionInterceptSelectCoin
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailSkipGuard));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.SkipDuelStep;

        readonly Defines.CoinType coinType = Defines.CoinType.Guard;

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
            var guardcoin = Defines.GetLocalizedString(Defines.StringEnum.GuardCoin);

            return GetLocalizedString(nameof(PlayerConditionDetailSkipGuard), new Dictionary<string, string>
            {
                { nameof(guardcoin), guardcoin},
                { nameof(count), count }
            });
        }

        public bool InterceptSelectCoin(DuelManager duelManager, int targetPlayerNo, Defines.CoinType selectCoinType, Player player)
        {
            if (!selectCoinType.HasFlag(coinType)) return false;
            if (targetPlayerNo != player.PlayerNo) return false;
            if (!player.ConditionList.IsEffectStatus<PlayerConditionDetailSkipGuard>(duelManager.DuelData)) return false;

            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = player.PlayerNo,
                ParticleType = Defines.ParticleType.SkipDuelStep,
            });

            duelManager.RegistDuelEventAction(new ActionRemovePlayerCondition()
            {
                PlayerNo = player.PlayerNo,
                PlayerConditionName = nameof(PlayerConditionDetailSkipGuard),
            });

            return true;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailSkipGuard), value);
        }
    }
}
