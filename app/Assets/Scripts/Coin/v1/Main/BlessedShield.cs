using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BlessedShield : Coin
    {
        public const string name = "v1.BlessedShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.BlessedShield(name) };

        public override string PrefabName { get { return "BlessedShield"; } }

        public override float PositionY => 0.05f;
    }
}
