using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MyTrashToExclusion : Coin
    {
        public const string name = "v1.MyTrashToExclusion";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.MyTrashToExclusion(name) };

        public override string PrefabName { get { return "MyTrashToExclusion"; } }
    }
}
