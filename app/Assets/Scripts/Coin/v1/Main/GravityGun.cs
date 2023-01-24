using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class GravityGun : Coin
    {
        public const string name = "v1.GravityGun";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.GravityGun(name) };

        public override string PrefabName { get { return "GravityGun"; } }

        public override float PositionY { get; } = -0.25f;

        public override float RotateY => 90f;
    }
}
