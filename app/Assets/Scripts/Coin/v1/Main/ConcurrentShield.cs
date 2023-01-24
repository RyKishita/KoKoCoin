using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ConcurrentShield : Coin
    {
        public const string name = "v1.ConcurrentShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ConcurrentShield(name) };

        public override string PrefabName { get { return "ConcurrentShield"; } }

        public override float PositionY => 0.05f;
    }
}
