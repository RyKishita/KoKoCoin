using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SwapSetBySideArea : Coin
    {
        public const string name = "v1.SwapSetBySideArea";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.SwapSetBySideArea(name) };

        public override string PrefabName => "SwapSetBySideArea";
    }
}
