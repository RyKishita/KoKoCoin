using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Cannon : Coin
    {
        public const string name = "v1.Cannon";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.Cannon(name) };

        public override string PrefabName { get { return "Cannon"; } }

        public override float RotateY => 90f;
    }
}
