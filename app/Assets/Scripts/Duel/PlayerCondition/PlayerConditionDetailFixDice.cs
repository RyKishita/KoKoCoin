using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailFixDice : PlayerConditionDetail, IPlayerConditionInterceptDice
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailFixDice));

        public override bool IsGood(PlayerCondition playerCondition) => true;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.MovePlayer;

        public override string MakeExplain()
        {
            return MakeExplain(Defines.GetLocalizedString(Defines.StringEnum.Count));
        }

        public override string MakeExplain(DuelData duelData, PlayerCondition playerCondition)
        {
            return MakeExplain(playerCondition.Value.ToString());
        }

        string MakeExplain(string value)
        {
            return GetLocalizedString(nameof(PlayerConditionDetailFixDice), new Dictionary<string, string>
            {
                { nameof(value), value }
            });
        }

        public bool IsInterceptDice(DuelData duelData, int targetPlayerNo, Player player)
        {
            return targetPlayerNo == player.PlayerNo &&
                    player.ConditionList.Has<PlayerConditionDetailFixDice>();
        }

        public UniTask<int> InterceptDice(DuelManager duelManager, Player player)
        {
            duelManager.RegistDuelEventAction(new DuelEvent.ActionRemovePlayerCondition()
            {
                PlayerNo = player.PlayerNo,
                PlayerConditionName = nameof(PlayerConditionDetailFixDice)
            });

            var pc = player.ConditionList.GetItem<PlayerConditionDetailFixDice>();
            return UniTask.FromResult(pc.Value);
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailFixDice), value);
        }
    }
}
