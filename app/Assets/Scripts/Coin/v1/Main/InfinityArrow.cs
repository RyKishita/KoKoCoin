using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class InfinityArrow : Coin
    {
        public const string name = "v1.InfinityArrow";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.InfinityArrow(name) };

        public override string PrefabName { get { return "InfinityArrow"; } }

        public override float RotateY => 90f;
    }
}
