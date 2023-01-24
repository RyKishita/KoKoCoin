using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Manure : Coin
    {
        public const string name = "v1.Manure";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.Manure(name) };

        public override string PrefabName { get { return "Manure"; } }
    }
}
