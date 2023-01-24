using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ProtoFullReflectionShield : Coin
    {
        public const string name = "v1.ProtoFullReflectionShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.CoinBodyGuardFullReflectionLoopTurn(name, 4) };

        public override string PrefabName { get { return "ProtoFullReflectionShield"; } }

        public override float PositionY => 0.05f;
    }
}
