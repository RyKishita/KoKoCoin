using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SmallPhysicsShield : Coin
    {
        public const string name = "v1.SmallPhysicalShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.PhysicsShield(name, 400, string.Empty) };

        public override string PrefabName { get { return "SmallPhysicalShield"; } }

        public override float PositionY => .05f;
    }
}
