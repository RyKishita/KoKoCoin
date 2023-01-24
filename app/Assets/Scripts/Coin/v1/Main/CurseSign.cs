using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class CurseSign : Coin
    {
        public const string name = "v1.CurseSign";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.CurseSign(name) };

        public override string PrefabName { get { return "CurseSign"; } }
    }
}
