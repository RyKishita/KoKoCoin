using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class AlianDragon : Coin
    {
        public const string name = "v1.AlianDragon";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AlianDragon(name) };

        public override string PrefabName { get { return "AlianDragon"; } }

        public override float PositionY => -.4f;

        public override float RotateY => 135f;
    }
}
