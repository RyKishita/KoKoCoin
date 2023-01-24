using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MakeCopyOfSetCoin : Coin
    {
        public const string name = "v1.MakeCopyOfSetCoin";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.MakeCopyOfSetCoin(name, Defines.CoinPosition.Hand) };

        public override string PrefabName { get { return "MakeCopyOfSetCoin"; } }
    }
}
