using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class DecrementDice : Coin
    {
        public const string name = "v1.DecrementDice";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AdjustDice(name, -1) };

        public override string PrefabName { get { return "DecrementDice"; } }

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .9f;
    }
}
