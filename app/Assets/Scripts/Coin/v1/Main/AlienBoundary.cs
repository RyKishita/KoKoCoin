using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class AlienBoundary : Coin
    {
        public const string name = "v1.AlienBoundary";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AlienBoundary(name) };

        public override string PrefabName { get { return "AlienBoundary"; } }
    }
}
