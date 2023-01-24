using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BloodDagger : Coin
    {
        public const string name = "v1.BloodDagger";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.BloodDagger(name) };

        public override string PrefabName { get { return "BloodDagger"; } }

        public override float RotateY => 270f;
    }
}
