using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PortableRailgun : Coin
    {
        public const string name = "v1.PortableRailgun";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.PortableRailgun(name) };

        public override string PrefabName { get { return "PortableRailgun"; } }

        public override float PositionX { get; } = 0.1f;
        public override float PositionY { get; } = -0.1f;

        public override float RotateY { get; } = 90f;

        public override float RotateX { get; } = 315f;
    }
}
