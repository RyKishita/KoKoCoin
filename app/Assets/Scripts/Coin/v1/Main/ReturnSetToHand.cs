using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ReturnSetToHand : Coin
    {
        public const string name = "v1.ReturnSetToHand";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ReturnSetToHand(name) };

        public override string PrefabName => "ReturnSetToHand";
    }
}
