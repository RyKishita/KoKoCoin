using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class HeavyAttack : Coin
    {
        public const string name = "v1.HeavyAttack";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.HeavyAttack(name) };

        public override string PrefabName { get { return "HeavyAttack"; } }

        public override float PositionY => -.4f;
        public override float RotateY => 195;
        public override float ScaleCoinValue => 0.9f;
    }
}
