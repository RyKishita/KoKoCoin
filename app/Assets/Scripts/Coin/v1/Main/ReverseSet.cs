using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ReverseSet : Coin
    {
        public const string name = "v1.ReverseSet";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ReverseSet(name) };

        public override string PrefabName { get { return "ReverseSet"; } }

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .9f;
    }
}
