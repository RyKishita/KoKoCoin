using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BricksWall : Coin
    {
        public const string name = "v1.BricksWall";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.BricksWall(name) };

        public override string PrefabName { get { return "BricksWall"; } }

        public override float PositionY => -0.45f;
        public override float ScaleCoinValue => .9f;
    }
}
