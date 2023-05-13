using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Duel.PlayerCondition
{
    /// <summary>
    /// 規定値を超えると所持コインを全て捨て、カウントはゼロに
    /// </summary>
    class PlayerConditionDetailVirus : PlayerConditionDetail, IPlayerConditionTriggerEvent, IPlayerCondtionCount
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailVirus));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.Virus;

        public override string MakeExplain()
        {
            return MakeExplain(Defines.GetLocalizedString(Defines.StringEnum.ConditionCount));
        }

        public override string MakeExplain(DuelData duelData, PlayerCondition playerCondition)
        {
            return MakeExplain(playerCondition.Value.ToString());
        }

        string MakeExplain(string count)
        {
            var handcoin = Defines.GetLocalizedString(Defines.CoinPosition.Hand);
            var trashcoin = Defines.GetLocalizedString(Defines.CoinPosition.Trash);

            return GetLocalizedString(nameof(PlayerConditionDetailVirus), new Dictionary<string, string>
            {
                { nameof(count), count },
                { nameof(handcoin), handcoin },
                { nameof(trashcoin), trashcoin },
            });
        }

        public bool IsReceiveEvent(DuelData duelData, Player player, After duelEvent)
        {
            return duelEvent is AfterStep afterStep &&
                    afterStep.GamePhase == Defines.DuelPhase.ターン終了 &&
                    player.ConditionList.IsEffectStatus<PlayerConditionDetailVirus>(duelData);
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, Player player, After duelEvent)
        {
            if (IsReceiveEvent(duelManager.DuelData, player, duelEvent))
            {
                var coins = player.Hand.Items.Select(item => item.ID).ToList();
                if (coins.Any())
                {
                    duelManager.FocusPlayer(player.PlayerNo);

                    duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                    {
                        PlayerNo = player.PlayerNo,
                        ParticleType = Defines.ParticleType.Virus
                    });

                    duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
                    {
                        CoinIDs = coins,
                        DstCoinPosition = Defines.CoinPosition.Trash,
                        CoinMoveReason = Defines.CoinMoveReason.PlayerCondition,
                    });

                    duelManager.RegistDuelEventAction(new ActionRemovePlayerCondition()
                    {
                        PlayerNo = player.PlayerNo,
                        PlayerConditionName = nameof(PlayerConditionDetailVirus)
                    });
                }
            }
            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailVirus), value);
        }
    }
}
