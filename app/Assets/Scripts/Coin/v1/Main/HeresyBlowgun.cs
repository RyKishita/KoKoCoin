using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class HeresyBlowgun : Coin
    {
        public const string name = "v1.HeresyBlowgun";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.HeresyBlowgun(name) };

        public override string PrefabName { get { return "HeresyBlowgun"; } }

        public override float PositionX { get; } = 0.1f;
        public override float PositionY { get; } = -0.2f;
        public override float RotateX => 45f;
        public override float RotateY => 225f;
    }
}
