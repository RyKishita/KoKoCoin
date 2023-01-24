using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class DaemonHead : Coin
    {
        public const string name = "v1.DaemonHead";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.DaemonHead(name) };

        public override string PrefabName { get { return "DaemonHead"; } }

        public override float PositionY => -0.3f;
    }
}
