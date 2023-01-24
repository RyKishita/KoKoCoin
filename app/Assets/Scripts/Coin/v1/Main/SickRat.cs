using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SickRat : Coin
    {
        public const string name = "v1.SickRat";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.SickRat(name) };

        public override string PrefabName { get { return "SickRat"; } }

        public override float PositionY { get; } = -0.3f;

        public override float RotateY => 135f;
    }
}
