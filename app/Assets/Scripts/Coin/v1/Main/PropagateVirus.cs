using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PropagateVirus : Coin
    {
        public const string name = "v1.PropagateVirus";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.PropagateVirus(name) };

        public override string PrefabName { get { return "PropagateVirus"; } }
    }
}
