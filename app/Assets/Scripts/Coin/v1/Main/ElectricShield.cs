using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ElectricShield : Coin
    {
        public const string name = "v1.ElectricShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ElectricShield(name) };

        public override string PrefabName { get { return "ElectricShield"; } }

        public override float PositionY => 0.05f;
    }
}
