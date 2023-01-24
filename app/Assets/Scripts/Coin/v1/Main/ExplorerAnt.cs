using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ExplorerAnt : Coin
    {
        public const string name = "v1.ExplorerAnt";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ExplorerAnt(name) };

        public override string PrefabName { get { return "ExplorerAnt"; } }

        public override float PositionY { get; } = -0.3f;

        public override float RotateY => 135f;
    }
}
