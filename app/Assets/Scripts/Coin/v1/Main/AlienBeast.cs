using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class AlienBeast : Coin
    {
        public const string name = "v1.AlienBeast";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AlienBeast(name) };

        public override string PrefabName { get { return "AlienBeast"; } }

        public override float PositionY => -.4f;
        public override float RotateY => 135f;
        public override float ScaleCoinValue => .8f;
    }
}
