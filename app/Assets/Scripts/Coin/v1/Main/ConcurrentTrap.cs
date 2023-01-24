using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ConcurrentTrap : Coin
    {
        public const string name = "v1.ConcurrentTrap";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ConcurrentTrap(name) };

        public override string PrefabName { get { return "ConcurrentTrap"; } }

        public override float PositionY => .05f;
        public override float RotateX => 60f;
        public override float ScaleCoinValue => .9f;
    }
}
