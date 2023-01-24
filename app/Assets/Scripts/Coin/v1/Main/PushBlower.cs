using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PushBlower : Coin
    {
        public const string name = "v1.PushBlower";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.PushBlower(name) };

        public override string PrefabName { get { return "PushBlower"; } }

        public override float PositionY { get; } = -0.2f;
        public override float RotateY { get; } = 90f;
        public override float ScaleCoinValue => .7f;
    }
}
