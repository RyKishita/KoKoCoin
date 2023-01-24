using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class UndeadSoldier : Coin
    {
        public const string name = "v1.UndeadSoldier";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.UndeadSoldier(name) };

        public override string PrefabName { get { return "UndeadSoldier"; } }
    }
}
