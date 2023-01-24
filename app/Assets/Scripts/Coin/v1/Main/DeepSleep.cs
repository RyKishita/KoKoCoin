using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class DeepSleep : Coin
    {
        public const string name = "v1.DeepSleep";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.DeepSleep(name) };

        public override string PrefabName { get { return "DeepSleep"; } }
    }
}
