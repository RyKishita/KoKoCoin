using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ElectricKing : Coin
    {
        public const string name = "v1.ElectricKing";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ElectricKing(name) };

        public override string PrefabName { get { return "ElectricKing"; } }
    }
}
