using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Duel.PlayerCondition
{
    class PlayerConditionDetailGravity : PlayerConditionDetail, IPlayerConditionChangeDice
    {
        public override string DisplayName => GetLocalizedStringName(nameof(PlayerConditionDetailGravity));

        public override bool IsGood(PlayerCondition playerCondition) => false;

        public override Defines.ParticleType GetParticleType(PlayerCondition playerCondition) => Defines.ParticleType.Gravity;

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
            return GetLocalizedString(nameof(PlayerConditionDetailGravity), new Dictionary<string, string>
            {
                { nameof(count), count }
            });
        }

        public static PlayerCondition CreatePlayerCondition(int value)
        {
            return new PlayerCondition(nameof(PlayerConditionDetailGravity), value);
        }

        public int GetChangeDice(DuelData duelData, PlayerCondition playerCondition)
        {
            return (playerCondition.Value / duelData.GetConditionCount()) * -1;
        }
    }
}
