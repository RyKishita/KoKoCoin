using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class VirusScatter : Coin
    {
        public const string name = "v1.VirusScatter";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.VirusScatter(name) };

        public override string PrefabName { get { return "VirusScatter"; } }

        public override float PositionY { get; } = -0.2f;

        public override float RotateY { get; } = 90f;

        public override float RotateX { get; } = 315f;
    }
}
