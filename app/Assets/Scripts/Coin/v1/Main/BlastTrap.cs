using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BlastTrap : Coin
    {
        public const string name = "v1.BlastTrap";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.BlastTrap(name) };

        public override string PrefabName { get { return "BlastTrap"; } }

        public override float PositionY { get; } = -0.1f;

        public override float RotateX { get; } = 45f;
    }
}
