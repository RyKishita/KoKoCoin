using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MyTrashToStock : Coin
    {
        public const string name = "v1.MyTrashToStock";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.MyCoinToStock(name, Defines.CoinPosition.Trash) };

        public override string PrefabName { get { return "MyTrashToStock"; } }
    }
}
