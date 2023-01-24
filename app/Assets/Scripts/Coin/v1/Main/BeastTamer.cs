using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BeastTamer : Coin
    {
        public const string name = "v1.BeastTamer";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AppendSetCoin(name, Defines.CoinTag.獣, 300) };

        public override string PrefabName { get { return "BeastTamer"; } }

        public override float RotateY => 135f;

        public override float PositionY => -0.4f;

        public override float ScaleCoinValue => 0.9f;
    }
}
