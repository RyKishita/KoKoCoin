using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MachineRider : Coin
    {
        public const string name = "v1.MachineRider";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AppendSetCoin(name, Defines.CoinTag.機械, 300) };

        public override string PrefabName { get { return "MachineRider"; } }
    }
}
