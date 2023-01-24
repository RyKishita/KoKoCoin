using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Decoy : Coin
    {
        public const string name = "v1.Decoy";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.Decoy(name, "Decoy") };

        public override string PrefabName { get { return "Decoy"; } }
    }
}
