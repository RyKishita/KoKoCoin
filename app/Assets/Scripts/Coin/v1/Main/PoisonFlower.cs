using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PoisonFlower : Coin
    {
        public const string name = "v1.PoisonFlower";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.PoisonFlower(name) };

        public override string PrefabName { get { return "PoisonFlower"; } }
    }
}
