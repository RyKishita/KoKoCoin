using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PoisonShield : Coin
    {
        public const string name = "v1.PoisonShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.PoisonShield(name) };

        public override string PrefabName { get { return "PoisonShield"; } }

        public override float PositionY => 0.05f;
    }
}
