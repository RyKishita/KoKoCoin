using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PrototypeFullProtectionShield : Coin
    {
        public const string name = "v1.PrototypeFullProtectionShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.CoinBodyGuardFullProtectionLoopTurn(name, 3) };

        public override string PrefabName { get { return "PrototypeFullProtectionShield"; } }

        public override float PositionY => 0.1f;
    }
}
