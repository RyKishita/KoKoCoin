using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ProgressTurnAreaSetCoins : Coin
    {
        public const string name = "v1.ProgressTurnAreaSetCoins";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ProgressTurnSetCoin(name, 3) };

        public override string PrefabName => "ProgressTurnAreaSetCoins";
    }
}
