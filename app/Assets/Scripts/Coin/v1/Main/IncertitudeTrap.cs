using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class IncertitudeTrap : Coin
    {
        public const string name = "v1.IncertitudeTrap";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.IncertitudeTrap(name) };

        public override string PrefabName { get { return "IncertitudeTrap"; } }

        public override float PositionY { get; } = -0.3f;
    }
}
