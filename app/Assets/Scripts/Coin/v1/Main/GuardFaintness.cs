using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class GuardFaintness : Coin
    {
        public const string name = "v1.GuardFaintness";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.GuardFaintness(name) };

        public override string PrefabName { get { return "GuardFaintness"; } }

        public override float PositionY => 0.1f;
    }
}
