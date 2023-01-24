﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class DozeFog : Coin
    {
        public const string name = "v1.DozeFog";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.DozeFog(name) };

        public override string PrefabName { get { return "DozeFog"; } }

        public override float ScaleCoinValue => 0.9f;
    }
}
