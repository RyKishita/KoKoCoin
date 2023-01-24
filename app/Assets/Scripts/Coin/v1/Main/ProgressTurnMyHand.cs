using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ProgressTurnMyHand : Coin
    {
        public const string name = "v1.ProgressTurnMyHand";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ProgressTurnHandCoinsByPlayer(name, true, 1) };

        public override string PrefabName => "ProgressTurnMyHand";
    }
}
