using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ImpactAbsorbeShield : Coin
    {
        public const string name = "v1.ImpactAbsorbeShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ImpactAbsorbeShield(name) };

        public override string PrefabName { get { return "ImpactAbsorbeShield"; } }

        public override float PositionY => 0.05f;
        public override float ScaleCoinValue => .6f;
    }
}
