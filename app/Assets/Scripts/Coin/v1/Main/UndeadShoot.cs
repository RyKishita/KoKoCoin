using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class UndeadShoot : Coin
    {
        public const string name = "v1.UndeadShoot";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.UndeadShoot(name) };

        public override string PrefabName { get { return "UndeadShoot"; } }

        public override float PositionY => -0.4f;
        public override float ScaleCoinValue => 0.7f;
    }
}
