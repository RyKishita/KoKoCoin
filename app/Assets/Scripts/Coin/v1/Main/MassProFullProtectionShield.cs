using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MassProFullProtectionShield : Coin
    {
        public const string name = "v1.MassProFullProtectionShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.CoinBodyGuardFullProtectionTurn(name, 5) };

        public override string PrefabName { get { return "MassProFullProtectionShield"; } }

        public override float PositionY => 0.05f;
    }
}
