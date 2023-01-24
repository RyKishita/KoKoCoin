﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Confusion : Coin
    {
        public const string name = "v1.Confusion";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.Confusion(name, 2, "ConfusionA") };

        public override string PrefabName { get { return "Confusion"; } }
    }
}
