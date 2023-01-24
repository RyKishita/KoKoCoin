using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin
{
    class CoinBackground : Coin, IExtraCoin
    {
        public const string name = "CoinBackground";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.NotUse(name) };

        public override string PrefabName => "Coin";
    }
}
