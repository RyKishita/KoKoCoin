using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BioShield : Coin
    {
        public const string name = "v1.BioShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.BioShield(name) };

        public override string PrefabName { get { return "BioShield"; } }

        public override float PositionY => 0.05f;
    }
}
