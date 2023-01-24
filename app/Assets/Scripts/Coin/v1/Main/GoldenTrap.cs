using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class GoldenTrap : Coin
    {
        public const string name = "v1.GoldenTrap";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.GoldenTrap(name) };

        public override string PrefabName { get { return "GoldenTrap"; } }

        public override float PositionY { get; } = -0.1f;

        public override float RotateX { get; } = 45f;
    }
}
