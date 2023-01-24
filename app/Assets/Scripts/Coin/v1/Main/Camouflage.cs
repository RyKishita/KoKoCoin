using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Camouflage : Coin
    {
        public const string name = "v1.Camouflage";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.Camouflage(name, "Camouflage") };

        public override string PrefabName { get { return "Camouflage"; } }
    }
}
