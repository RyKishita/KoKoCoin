﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MoanBeast : Coin
    {
        public const string name = "v1.MoanBeast";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.MoanBeast(name) };

        public override string PrefabName { get { return "MoanBeast"; } }

        public override float PositionY => -.4f;

        public override float RotateY => 45f;
    }
}
