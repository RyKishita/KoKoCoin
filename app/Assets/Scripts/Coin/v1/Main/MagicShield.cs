using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MagicShield : Coin
    {
        public const string name = "v1.MagicShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.MagicShield(name, 400) };

        public override string PrefabName { get { return "MagicShield"; } }

        public override float PositionY => 0.05f;
    }
}
