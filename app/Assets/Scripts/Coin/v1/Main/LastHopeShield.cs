using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class LastHopeShield : Coin
    {
        public const string name = "v1.LastHopeShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.LastHopeShield(name) };

        public override string PrefabName { get { return "LastHopeShield"; } }

        public override float PositionY => 0.1f;
    }
}
