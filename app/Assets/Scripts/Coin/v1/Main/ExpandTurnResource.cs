using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ExpandTurnResource : Coin
    {
        public const string name = "v1.ExpandTurnResource";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ExpandTurnResource(name) };

        public override string PrefabName { get { return "ExpandTurnResource"; } }

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .9f;
    }
}
