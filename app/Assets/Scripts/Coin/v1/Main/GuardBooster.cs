using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class GuardBooster : Coin
    {
        public const string name = "v1.GuardBooster";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.GuardBooster(name, 400) };

        public override string PrefabName { get { return "GuardBooster"; } }
    }
}
