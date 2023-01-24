using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BioBlaster : Coin
    {
        public const string name = "v1.BioBlaster";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.BioBlaster(name) };

        public override string PrefabName { get { return "BioBlaster"; } }

        public override float PositionY => -.4f;
        public override float RotateY => 90f;
        public override float ScaleCoinValue => 0.9f;
    }
}
