using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ToyGun : Coin
    {
        public const string name = "v1.ToyGun";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ToyGun(name) };

        public override string PrefabName { get { return "ToyGun"; } }

        public override float PositionX { get; } = 0.3f;

        public override float PositionY { get; } = -0.3f;

        public override float RotateX { get; } = 315f;

        public override float RotateY { get; } = 90f;

    }
}
