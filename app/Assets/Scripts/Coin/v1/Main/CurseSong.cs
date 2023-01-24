using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class CurseSong : Coin
    {
        public const string name = "v1.CurseSong";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.CurseSong(name) };

        public override string PrefabName { get { return "CurseSong"; } }

        public override float PositionY => -.3f;
    }
}
