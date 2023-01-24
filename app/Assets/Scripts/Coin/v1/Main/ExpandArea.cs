using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ExpandArea : Coin
    {
        public const string name = "v1.ExpandArea";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ExpandArea(name) };

        public override string PrefabName { get { return "ExpandArea"; } }

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .9f;
    }
}
