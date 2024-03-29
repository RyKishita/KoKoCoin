﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class FireWall : Coin
    {
        public const string name = "v1.FireWall";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.FireWall(name) };

        public override string PrefabName { get { return "FireWall"; } }

        public override float ScaleCoinValue => .8f;

        public override float PositionY => -.4f;
    }
}
