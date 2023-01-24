using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class GravityMaker : Coin
    {
        public const string name = "v1.GravityMaker";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.GravityMaker(name) };

        public override string PrefabName { get { return "GravityMaker"; } }
    }
}
