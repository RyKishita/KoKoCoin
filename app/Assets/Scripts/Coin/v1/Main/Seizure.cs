using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Seizure : Coin
    {
        public const string name = "v1.Seizure";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.Seizure(name, 20) };

        public override string PrefabName { get { return "Seizure"; } }
    }
}
