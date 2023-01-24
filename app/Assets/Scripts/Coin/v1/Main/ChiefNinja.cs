using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ChiefNinja : Coin
    {
        public const string name = "v1.ChiefNinja";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.Ninja(name, 1, 300) };

        public override string PrefabName { get { return "ChiefNinja"; } }
    }
}
