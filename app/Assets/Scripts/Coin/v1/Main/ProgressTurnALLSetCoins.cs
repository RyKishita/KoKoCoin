using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ProgressTurnALLSetCoins : Coin
    {
        public const string name = "v1.ProgressTurnALLSetCoins";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ProgressTurnALLSetCoins(name, 1) };

        public override string PrefabName => "ProgressTurnALLSetCoins";
    }
}
