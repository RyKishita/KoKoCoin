using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class DimensionBeacon : Coin
    {
        public const string name = "v1.DimensionBeacon";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Scripts.Coin.Body.NotUse(name) };

        public override string PrefabName { get { return "DimensionBeacon"; } }
    }
}
