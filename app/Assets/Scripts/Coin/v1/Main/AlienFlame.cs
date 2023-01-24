using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class AlienFlame : Coin
    {
        public const string name = "v1.AlienFlame";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AlienFlame(name) };

        public override string PrefabName { get { return "AlienFlame"; } }
    }
}
