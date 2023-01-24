using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SetBooster : Coin
    {
        public const string name = "v1.SetBooster";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.SetBooster(name, 200) };

        public override string PrefabName { get { return "SetBooster"; } }
    }
}
