﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ProtoFullProtectionShield : Coin
    {
        public const string name = "v1.ProtoFullProtectionShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.CoinBodyGuardFullProtectionLoopTurn(name, 3) };

        public override string PrefabName { get { return "ProtoFullProtectionShield"; } }

        public override float PositionY => 0.1f;
    }
}
