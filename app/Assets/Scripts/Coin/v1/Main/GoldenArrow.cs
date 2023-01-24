using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class GoldenArrow : Coin
    {
        public const string name = "v1.GoldenArrow";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.GoldenArrow(name) };

        public override string PrefabName { get { return "GoldenArrow"; } }

        public override float RotateY => 90f;
    }
}
