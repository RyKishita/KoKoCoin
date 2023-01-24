using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class GermHole : Coin
    {
        public const string name = "v1.GermHole";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.GermHole(name) };

        public override string PrefabName { get { return "GermHole"; } }

        public override float PositionY { get; } = -0.1f;

        public override float RotateX { get; } = 45f;

        public override float ScaleCoinValue => 0.9f;
    }
}
