using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class DaemonBody : Coin
    {
        public const string name = "v1.DaemonBody";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.DaemonBody(name) };

        public override string PrefabName { get { return "DaemonBody"; } }
    }
}
