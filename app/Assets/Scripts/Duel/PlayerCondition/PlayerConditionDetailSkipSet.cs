using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailSkipSet : PlayerConditionDetail, IPlayerConditionInterceptSelectCoin
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailSkipSet));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.SkipDuelStep;

        readonly Defines.CoinType coinType = Defines.CoinType.Set;

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
            var settedcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);

            return GetLocalizedString(nameof(PlayerConditionDetailSkipSet), new Dictionary<string, string>
            {
                { nameof(settedcoin), settedcoin},
                { nameof(count), count }
            });
        }

        public bool InterceptSelectCoin(DuelManager duelManager, int targetPlayerNo, Defines.CoinType selectCoinType, Player player)
        {
            if (!selectCoinType.HasFlag(coinType)) return false;
            if (targetPlayerNo != player.PlayerNo) return false;
            if (!player.ConditionList.IsEffectStatus<PlayerConditionDetailSkipSet>(duelManager.DuelData)) return false;

            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = player.PlayerNo,
                ParticleType = Defines.ParticleType.SkipDuelStep,
            });

            duelManager.RegistDuelEventAction(new ActionRemovePlayerCondition()
            {
                PlayerNo = player.PlayerNo,
                PlayerConditionName = nameof(PlayerConditionDetailSkipSet)
            });

            return true;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailSkipSet), value);
        }
    }
}
