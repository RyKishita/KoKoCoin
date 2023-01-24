using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class IronLongSword : Coin
    {
        public const string name = "v1.IronLongSword";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.IronLongSword(name) };

        public override string PrefabName { get { return "IronLongSword"; } }

        public override float PositionX => -0.4f;

        public override float PositionY => -0.3f;

        public override float RotateX => 315f;

        public override float RotateY => 270f;
    }
}
