using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class HeresyIdol : Coin
    {
        public const string name = "v1.HeresyIdol";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.HeresyIdol(name) };

        public override string PrefabName { get { return "HeresyIdol"; } }

        public override float PositionY => -0.4f;
        public override float ScaleCoinValue => 0.9f;

        public override float ScaleModelValue => .5f;
    }
}
