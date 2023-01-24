using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SmallPoisonTrap : Coin
    {
        public const string name = "v1.SmallPoisonTrap";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.SmallPoisonTrap(name) };

        public override string PrefabName { get { return "SmallPoisonTrap"; } }

        public override float PositionY => -.4f;

        public override float RotateX { get; } = 45f;
    }
}
