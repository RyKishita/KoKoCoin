﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ChiefNinjaSword : Coin
    {
        public const string name = "v1.ChiefNinjaSword";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.NinjaSword(name, ChiefNinja.name, 300) };

        public override string PrefabName { get { return "ChiefNinjaSword"; } }

        public override float RotateY => 270f;
    }
}
