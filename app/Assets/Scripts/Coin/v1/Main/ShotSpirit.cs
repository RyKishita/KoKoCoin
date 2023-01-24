using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ShotSpirit : Coin
    {
        public const string name = "v1.ShotSpirit";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ShotSpirit(name) };

        public override string PrefabName { get { return "ShotSpirit"; } }

        public override float RotateY => 135f;
    }
}
