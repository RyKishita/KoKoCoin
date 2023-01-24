using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Shield : Coin
    {
        public const string name = "v1.Shield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.BasicShield(name) };

        public override string PrefabName { get { return "Shield"; } }

        public override float PositionY => 0.05f;
    }
}
