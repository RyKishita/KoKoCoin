using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class AlianMachine : Coin
    {
        public const string name = "v1.AlianMachine";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AlianMachine(name) };

        public override string PrefabName { get { return "AlianMachine"; } }
    }
}
