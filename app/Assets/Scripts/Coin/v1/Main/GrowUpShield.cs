using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class GrowUpShield : Coin
    {
        public const string name = "v1.GrowUpShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.GrowUpShield(name) };

        public override string PrefabName { get { return "GrowUpShield"; } }

        public override float ScaleCoinValue => .9f;

        public override float PositionY => 0.05f;
    }
}
