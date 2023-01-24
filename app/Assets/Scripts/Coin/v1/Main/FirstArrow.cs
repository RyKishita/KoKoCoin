using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class FirstArrow : Coin
    {
        public const string name = "v1.FirstArrow";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.FirstArrow(name) };

        public override string PrefabName { get { return "FirstArrow"; } }

        public override float RotateY => 90f;
    }
}
