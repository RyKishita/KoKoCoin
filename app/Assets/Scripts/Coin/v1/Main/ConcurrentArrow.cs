using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ConcurrentArrow : Coin
    {
        public const string name = "v1.ConcurrentArrow";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ConcurrentArrow(name) };

        public override string PrefabName { get { return "ConcurrentArrow"; } }

        public override float RotateY => 90f;
    }
}
