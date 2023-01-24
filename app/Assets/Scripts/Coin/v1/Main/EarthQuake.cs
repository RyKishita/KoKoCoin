using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class EarthQuake : Coin
    {
        public const string name = "v1.EarthQuake";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.EarthQuake(name) };

        public override string PrefabName { get { return "EarthQuake"; } }

        public override float PositionY { get; } = -0.1f;
        public override float ScaleCoinValue => 0.8f;

        public override float RotateX { get; } = 45f;
    }
}
