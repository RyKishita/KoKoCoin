using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ChopperOfPoor : Coin
    {
        public const string name = "v1.ChopperOfPoor";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ChopperOfPoor(name) };

        public override string PrefabName { get { return "ChopperOfPoor"; } }

        public override float RotateY => 90f;
    }
}
