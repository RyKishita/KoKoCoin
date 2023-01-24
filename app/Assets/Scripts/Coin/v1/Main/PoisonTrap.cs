using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PoisonTrap : Coin
    {
        public const string name = "v1.PoisonTrap";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.PoisonTrap(name) };

        public override string PrefabName { get { return "PoisonTrap"; } }

        public override float PositionY { get; } = -0.1f;

        public override float RotateX { get; } = 45f;
    }
}
