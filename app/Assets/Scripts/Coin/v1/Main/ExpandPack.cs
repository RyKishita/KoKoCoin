using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ExpandPack : Coin
    {
        public const string name = "v1.ExpandPack";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ExpandPack(name) };

        public override string PrefabName { get { return "ExpandPack"; } }

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .9f;
    }
}
