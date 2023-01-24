using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class FlameAnt : Coin
    {
        public const string name = "v1.FlameAnt";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.FlameAnt(name) };

        public override string PrefabName { get { return "FlameAnt"; } }

        public override float PositionY { get; } = -0.3f;

        public override float RotateY => 135f;
    }
}
