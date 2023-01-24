using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class UndeadLeader : Coin
    {
        public const string name = "v1.UndeadLeader";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.UndeadLeader(name) };

        public override string PrefabName { get { return "UndeadLeader"; } }
    }
}
