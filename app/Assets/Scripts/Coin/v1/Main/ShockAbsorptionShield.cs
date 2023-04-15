using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ShockAbsorptionShield : Coin
    {
        public const string name = "v1.ShockAbsorptionShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ShockAbsorptionShield(name) };

        public override string PrefabName { get { return "ShockAbsorptionShield"; } }

        public override float PositionY => 0.05f;
        public override float ScaleCoinValue => .6f;
    }
}
