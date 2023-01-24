using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Scapegoat : Coin
    {
        public const string name = "v1.Scapegoat";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.Scapegoat(name) };

        public override string PrefabName { get { return "Scapegoat"; } }

        public override float PositionY { get; } = -0.3f;

        public override float RotateY => 135f;
    }
}
