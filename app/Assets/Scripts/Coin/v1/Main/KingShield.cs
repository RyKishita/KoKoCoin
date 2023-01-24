using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class KingShield : Coin
    {
        public const string name = "v1.KingShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.KingShield(name) };

        public override string PrefabName { get { return "KingShield"; } }

        public override float PositionY => 0.05f;
    }
}
