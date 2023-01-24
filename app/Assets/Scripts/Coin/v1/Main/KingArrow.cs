using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class KingArrow : Coin
    {
        public const string name = "v1.KingArrow";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.KingArrow(name) };

        public override string PrefabName { get { return "KingArrow"; } }

        public override float RotateY { get; } = 90f;
    }
}
