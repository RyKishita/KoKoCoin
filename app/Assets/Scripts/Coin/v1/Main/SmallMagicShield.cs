using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SmallMagicShield : Coin
    {
        public const string name = "v1.SmallMagicShield";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.MagicShield(name, 400, string.Empty) };

        public override string PrefabName { get { return "SmallMagicShield"; } }

        public override float PositionY => .05f;
    }
}
