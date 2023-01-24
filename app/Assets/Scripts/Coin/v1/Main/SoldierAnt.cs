using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SoldierAnt : Coin
    {
        public const string name = "v1.SoldierAnt";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.SoldierAnt(name) };

        public override string PrefabName { get { return "SoldierAnt"; } }

        public override float PositionY { get; } = -0.3f;

        public override float RotateY => 135f;
    }
}
