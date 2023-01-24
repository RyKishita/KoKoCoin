using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Qualm : Coin
    {
        public const string name = "v1.Qualm";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.Qualm(name) };

        public override string PrefabName { get { return "Qualm"; } }
    }
}
