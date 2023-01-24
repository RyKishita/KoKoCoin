using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PhantomSoldier : Coin
    {
        public const string name = "v1.PhantomSoldier";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.PhantomSoldier(name) };

        public override string PrefabName { get { return "PhantomSoldier"; } }
    }
}
