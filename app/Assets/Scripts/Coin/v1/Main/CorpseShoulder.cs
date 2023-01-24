using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class CorpseShoulder : Coin
    {
        public const string name = "v1.CorpseShoulder";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AppendSetCoin(name, Defines.CoinTag.人間, 500) };

        public override string PrefabName { get { return "CorpseShoulder"; } }

        public override float PositionY => -0.6f;

        public override float RotateY => 135f;
    }
}
