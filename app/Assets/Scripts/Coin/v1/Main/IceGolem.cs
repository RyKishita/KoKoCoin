using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class IceGolem : Coin
    {
        public const string name = "v1.IceGolem";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.IceGolem(name) };

        public override string PrefabName { get { return "IceGolem"; } }
    }
}
