using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailFixDice : PlayerConditionDetail, IPlayerConditionInterceptDice
    {
        static readonly string name = nameof(PlayerConditionDetailFixDice);

        public override string DisplayName => GetLocalizedStringName(name);

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
            return GetLocalizedString(name, new Dictionary<string, string>
            {
                { nameof(value), value }
            });
        }

        public int? InterceptDice(DuelManager duelManager, int targetPlayerNo, Player player)
        {
            if (targetPlayerNo != player.PlayerNo) return null;

            var pc = player.ConditionList.GetItem<PlayerConditionDetailFixDice>();
            if (pc == null) return null;

            duelManager.RegistDuelEventAction(new DuelEvent.ActionRemovePlayerCondition()
            {
                PlayerNo = player.PlayerNo,
                PlayerConditionName = name
            });

            return pc.Value;
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(name, value);
        }

        public override ValueTypeEnum ValueType => ValueTypeEnum.Override;
    }
}
