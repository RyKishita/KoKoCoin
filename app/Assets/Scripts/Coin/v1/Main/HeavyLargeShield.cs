using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class HeavyLargeShield : Coin
    {
        public const string name = "v1.HeavyLargeShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.HeavyLargeShield(name) };

        public override string PrefabName { get { return "HeavyLargeShield"; } }

        public override float PositionY => 0.1f;
    }
}
