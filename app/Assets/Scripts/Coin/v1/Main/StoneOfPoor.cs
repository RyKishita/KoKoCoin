using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class StoneOfPoor : Coin
    {
        public const string name = "v1.StoneOfPoor";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.StoneOfPoor(name) };

        public override string PrefabName { get { return "StoneOfPoor"; } }

        public override float RotateX => 10f;
        public override float RotateY => 190f;
        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .7f;
    }
}
