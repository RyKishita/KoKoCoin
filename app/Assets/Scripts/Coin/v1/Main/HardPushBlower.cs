using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class HardPushBlower : Coin
    {
        public const string name = "v1.HardPushBlower";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.HardPushBlower(name) };

        public override string PrefabName { get { return "HardPushBlower"; } }

        public override float PositionY { get; } = -0.2f;

        public override float RotateY => 90f;
    }
}
