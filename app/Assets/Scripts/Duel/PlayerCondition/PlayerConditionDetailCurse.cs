using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Duel.PlayerCondition
{

    /// <summary>
    /// 規定値を超えると敗北
    /// </summary>
    class PlayerConditionDetailCurse : PlayerConditionDetail, IPlayerConditionTriggerEvent, IPlayerCondtionCount
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailCurse));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.Curse;

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
            return GetLocalizedString(nameof(PlayerConditionDetailCurse), new Dictionary<string, string>
            {
                { nameof(count), count }
            });
        }

        public bool IsReceiveEvent(DuelData duelData, Player player, After after)
        {
            return after is AfterAddPlayerCondition afterAddCondition &&
                    afterAddCondition.PlayerNo == player.PlayerNo &&
                    afterAddCondition.PlayerCondition.InnerName == nameof(PlayerConditionDetailCurse) &&
                    player.ConditionList.IsEffectStatus<PlayerConditionDetailCurse>(duelData) ||
                    after is AfterMoveCoin; // 異常抑制法がフィールドから離れた事で発動する場合
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, Player player, After duelEvent)
        {
            if (player.ConditionList.IsEffectStatus<PlayerConditionDetailCurse>(duelManager.DuelData))
            {
                Utility.Functions.WriteLog(nameof(PlayerConditionDetailCurse), nameof(ReceiveEventAsync), player.PlayerNo);

                duelManager.FocusPlayer(player.PlayerNo);

                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = player.PlayerNo,
                    ParticleType = Defines.ParticleType.Curse
                });

                duelManager.RegistLoser(player.PlayerNo, Defines.DuelLoseReason.Curse);
            }
            return UniTask.CompletedTask;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailCurse), value);
        }
    }
}
