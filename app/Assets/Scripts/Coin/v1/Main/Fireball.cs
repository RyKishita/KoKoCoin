using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Fireball : Coin
    {
        public const string name = "v1.Fireball";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.Fireball(name) };

        public override string PrefabName { get { return "Fireball"; } }

        public override float PositionY => -.4f;
        public override float RotateY => 30f;
    }
}
