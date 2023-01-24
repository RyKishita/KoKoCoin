using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class VirusCarrier : Coin
    {
        public const string name = "v1.VirusCarrier";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.VirusCarrier(name) };

        public override string PrefabName { get { return "VirusCarrier"; } }
    }
}
