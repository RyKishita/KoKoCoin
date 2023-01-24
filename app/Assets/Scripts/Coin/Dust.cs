using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin
{
    class Dust : Coin, IExtraCoin
    {
        public const string name = "Dust";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.NotUse(name) };

        public override string PrefabName => "Default";
    }
}
