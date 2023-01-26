using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailAppendSetAttack : PlayerConditionDetail, IPlayerConditionTriggerEvent
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailAppendSetAttack));

        public override bool IsGood(PlayerCondition playerCondition) => 0 < playerCondition.Value;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => 0 < playerCondition.Value ? Defines.ParticleType.Buf : Defines.ParticleType.Debuf;

        public override string MakeExplain()
        {
            var settedattackcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedAttackCoin);

            var param = new Dictionary<string, string>
            {
                { nameof(settedattackcoin), settedattackcoin},
            };
            return GetLocalizedString(nameof(PlayerConditionDetailAppendSetAttack), param);
        }

        public override string MakeExplain(DuelData duelData, PlayerCondition playerCondition)
        {
            var settedattackcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedAttackCoin);

            int format = 0 < playerCondition.Value ? 0 : 1;
            var param = new Dictionary<string, string>
            {
                { nameof(settedattackcoin), settedattackcoin},
                { "value", Math.Abs(playerCondition.Value).ToString() },
            };

            return GetLocalizedString(nameof(PlayerConditionDetailAppendSetAttack), format, param);
        }

        bool IsReceiveEventEnd(Player player, After duelEvent)
        {
            return duelEvent is AfterEndUse afterUse &&
                    afterUse.Use.CoinData.OwnerPlayerNo == player.PlayerNo &&
                    afterUse.Use.GetCoinBody() is Coin.Body.SetAttack.Core;
        }

        public bool IsReceiveEvent(DuelData duelData, Player player, After duelEvent)
        {
            return IsReceiveEventEnd(player, duelEvent);
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, Player player, After duelEvent)
        {
            if (IsReceiveEventEnd(player, duelEvent))
            {
                var coinID = (duelEvent as AfterEndUse).Use.CoinData.ID;
                var pc = player.ConditionList.GetItem<PlayerConditionDetailAppendSetAttack>();

                duelManager.RegistDuelEventAction(new ActionEffectSetCoin()
                {
                    CoinIDs = new List<int>() { coinID },
                    ParticleType = 0 < pc.Value ? Defines.ParticleType.Buf : Defines.ParticleType.Debuf,
                });

                duelManager.RegistDuelEventAction(new ActionAddCoinStatus()
                {
                    CoinID = coinID,
                    CoinStatus = new CoinStatus.CoinStatusAppendValue() { Value = pc.Value }
                });

                duelManager.RegistDuelEventAction(new ActionRemovePlayerCondition()
                {
                    PlayerNo = player.PlayerNo,
                    PlayerConditionName = pc.InnerName
                });
            }

            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailAppendSetAttack), value);
        }
    }
}
