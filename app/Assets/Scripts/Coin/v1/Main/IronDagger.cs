using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class IronDagger : Coin
    {
        public const string name = "v1.IronDagger";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.IronDagger(name) };

        public override string PrefabName { get { return "IronDagger"; } }

        public override float PositionX => -0.2f;

        public override float PositionY => -0.2f;

        public override float RotateX => 315f;

        public override float RotateY => 270f;
    }
}
