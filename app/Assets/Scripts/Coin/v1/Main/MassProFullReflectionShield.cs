using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MassProFullReflectionShield : Coin
    {
        public const string name = "v1.MassProFullReflectionShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.CoinBodyGuardFullReflectionTurn(name, 10) };

        public override string PrefabName { get { return "MassProFullReflectionShield"; } }

        public override float PositionY => 0.05f;
    }
}
