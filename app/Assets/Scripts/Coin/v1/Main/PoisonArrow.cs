using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class PoisonArrow : Coin
    {
        public const string name = "v1.PoisonArrow";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.PoisonArrow(name) };

        public override string PrefabName { get { return "PoisonArrow"; } }

        public override float RotateY => 90f;
    }
}
