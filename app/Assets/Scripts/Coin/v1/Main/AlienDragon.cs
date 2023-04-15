using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class AlienDragon : Coin
    {
        public const string name = "v1.AlienDragon";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.AlienDragon(name) };

        public override string PrefabName { get { return "AlienDragon"; } }

        public override float PositionY => -.4f;

        public override float RotateY => 135f;
    }
}
