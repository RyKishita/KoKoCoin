using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class HeresyClaw : Coin
    {
        public const string name = "v1.HeresyClaw";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.HeresyClaw(name) };

        public override string PrefabName { get { return "HeresyClaw"; } }

        public override float PositionX => -0.2f;
        public override float PositionY => -0.3f;
        public override float RotateX => 45f;
        public override float RotateY => 45f;
    }
}
