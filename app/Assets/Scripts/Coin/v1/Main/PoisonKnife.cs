using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PoisonKnife : Coin
    {
        public const string name = "v1.PoisonKnife";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.PoisonKnife(name) };

        public override string PrefabName { get { return "PoisonKnife"; } }

        public override float RotateY => 270f;
    }
}
