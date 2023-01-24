using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ShotBooster : Coin
    {
        public const string name = "v1.ShotBooster";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ShotBooster(name, 200) };

        public override string PrefabName { get { return "ShotBooster"; } }
    }
}
