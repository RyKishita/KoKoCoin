using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BeastPackHead : Coin
    {
        public const string name = "v1.BeastPackHead";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.BeastPackHead(name) };

        public override string PrefabName { get { return "BeastPackHead"; } }

        public override float PositionY => -.4f;

        public override float RotateY => 135f;
    }
}
