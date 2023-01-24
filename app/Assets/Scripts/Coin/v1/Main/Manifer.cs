using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Manifer : Coin
    {
        public const string name = "v1.Manifer";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.Manifer(name) };

        public override string PrefabName { get { return "Manifer"; } }

        public override float PositionY => 0.1f;
        public override float RotateY { get; } = 210f;
    }
}
