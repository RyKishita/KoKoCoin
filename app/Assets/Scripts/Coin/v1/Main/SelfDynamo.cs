using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SelfDynamo : Coin
    {
        public const string name = "v1.SelfDynamo";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new []
        {
            new Body.GetCondition(name, Duel.PlayerCondition.PlayerConditionDetailElectric.CreatePlayerCondition(3), Defines.ParticleType.Electric)
        };

        public override string PrefabName { get { return "SelfDynamo"; } }
    }
}
