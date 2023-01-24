using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class StoneMissile : Coin
    {
        public const string name = "v1.StoneMissile";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.StoneMissile(name) };

        public override string PrefabName { get { return "StoneMissile"; } }

        public override float PositionY => -.45f;
        public override float RotateY => 195;
        public override float ScaleCoinValue => 0.8f;
    }
}
