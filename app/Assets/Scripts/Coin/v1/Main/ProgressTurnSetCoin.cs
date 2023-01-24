using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ProgressTurnSetCoin : Coin
    {
        public const string name = "v1.ProgressTurnSetCoin";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ProgressTurnCoinAreaSetCoin(name, 5) };

        public override string PrefabName => "ProgressTurnSetCoin";
    }
}
