using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class QueenAnt : Coin
    {
        public const string name = "v1.QueenAnt";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.QueenAnt(name) };

        public override string PrefabName { get { return "QueenAnt"; } }

        public override float PositionY { get; } = -0.3f;

        public override float RotateY => 135f;
    }
}
