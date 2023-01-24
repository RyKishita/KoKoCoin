using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class AfterShot : Coin
    {
        public const string name = "v1.AfterShot";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AfterShot(name) };

        public override string PrefabName { get { return "AfterShot"; } }

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .9f;
    }
}
