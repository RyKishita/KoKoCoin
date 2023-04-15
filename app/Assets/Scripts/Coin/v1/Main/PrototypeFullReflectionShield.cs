using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PrototypeFullReflectionShield : Coin
    {
        public const string name = "v1.PrototypeFullReflectionShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.CoinBodyGuardFullReflectionLoopTurn(name, 4) };

        public override string PrefabName { get { return "PrototypeFullReflectionShield"; } }

        public override float PositionY => 0.05f;
    }
}
