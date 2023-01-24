using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Buckler : Coin
    {
        public const string name = "v1.Buckler";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.Buckler(name) };

        public override string PrefabName { get { return "Buckler"; } }

        public override float PositionY => .05f;
    }
}
