using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class HeresyTrident : Coin
    {
        public const string name = "v1.HeresyTrident";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.HeresyTrident(name) };

        public override string PrefabName { get { return "HeresyTrident"; } }
    }
}
