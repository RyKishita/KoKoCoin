using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailAppendDirectAttack : PlayerConditionDetail, IPlayerConditionTriggerEvent
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailAppendDirectAttack));

        public override bool IsGood(PlayerCondition playerCondition) => 0 < playerCondition.Value;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => 0 < playerCondition.Value ? Defines.ParticleType.Buf : Defines.ParticleType.Debuf;

        public override string MakeExplain()
        {
            var directattackcoin = Defines.GetLocalizedString(Defines.StringEnum.DirectAttackCoin);

            var param = new Dictionary<string, string>
            {
                { nameof(directattackcoin), directattackcoin},
            };

            return GetLocalizedString(nameof(PlayerConditionDetailAppendDirectAttack), param);
        }

        public override string MakeExplain(DuelData duelData, PlayerCondition playerCondition)
        {
            var directattackcoin = Defines.GetLocalizedString(Defines.StringEnum.DirectAttackCoin);

            int format = 0 < playerCondition.Value ? 0 : 1;
            var param = new Dictionary<string, string>
            {
                { nameof(directattackcoin), directattackcoin},
                { "value", Math.Abs(playerCondition.Value).ToString() },
            };

            return GetLocalizedString(nameof(PlayerConditionDetailAppendDirectAttack), format, param);
        }

        bool IsReceiveEventStart(Player player, After duelEvent)
        {
            return duelEvent is AfterStartUse afterUse &&
                    afterUse.Use.CoinData.OwnerPlayerNo == player.PlayerNo &&
                    afterUse.Use.GetCoinBody() is Coin.Body.DirectAttack.Core;
        }

        bool IsReceiveEventEnd(Player player, After duelEvent)
        {
            return duelEvent is AfterEndUse afterUse &&
                    afterUse.Use.CoinData.OwnerPlayerNo == player.PlayerNo &&
                    afterUse.Use.GetCoinBody() is Coin.Body.DirectAttack.Core;
        }

        public bool IsReceiveEvent(DuelData duelData, Player player, After duelEvent)
        {
            return IsReceiveEventStart(player, duelEvent) || IsReceiveEventEnd(player, duelEvent);
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, Player player, After duelEvent)
        {
            if (IsReceiveEventStart(player, duelEvent))
            {
                var pc = player.ConditionList.GetItem<PlayerConditionDetailAppendDirectAttack>();
                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = player.PlayerNo,
                    ParticleType = 0 < pc.Value ? Defines.ParticleType.Buf : Defines.ParticleType.Debuf,
                });
            }

            if (IsReceiveEventEnd(player, duelEvent))
            {
                var pc = player.ConditionList.GetItem<PlayerConditionDetailAppendDirectAttack>();
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
            return new PlayerCondition(nameof(PlayerConditionDetailAppendDirectAttack), value);
        }
    }
}
