using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ManEater : Coin
    {
        public const string name = "v1.ManEater";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ManEater(name) };

        public override string PrefabName { get { return "ManEater"; } }

        public override float PositionY { get; } = -0.3f;

        public override float RotateY => 135f;
    }
}
