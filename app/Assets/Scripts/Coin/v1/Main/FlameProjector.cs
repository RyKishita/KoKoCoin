using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class FlameProjector : Coin
    {
        public const string name = "v1.FlameProjector";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.FlameProjector(name) };

        public override string PrefabName { get { return "FlameProjector"; } }

        public override float PositionX { get; } = 0.2f;

        public override float PositionY { get; } = -0.2f;

        public override float RotateY => 90f;

        public override float RotateX { get; } = 315f;
    }
}
