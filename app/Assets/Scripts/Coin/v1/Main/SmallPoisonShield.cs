using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SmallPoisonShield : Coin
    {
        public const string name = "v1.SmallPoisonShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.SmallPoisonShield(name) };

        public override string PrefabName { get { return "SmallPoisonShield"; } }

        public override float PositionY => .05f;
    }
}
