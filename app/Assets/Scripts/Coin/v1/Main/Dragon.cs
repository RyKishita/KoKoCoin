using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Dragon : Coin
    {
        public const string name = "v1.Dragon";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.Dragon(name) };

        public override string PrefabName { get { return "Dragon"; } }

        public override float PositionY => -.4f;

        public override float RotateY => 135f;
    }
}
