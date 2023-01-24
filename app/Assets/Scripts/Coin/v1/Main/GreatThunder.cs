using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class GreatThunder : Coin
    {
        public const string name = "v1.GreatThunder";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.GreatThunder(name) };

        public override string PrefabName { get { return "GreatThunder"; } }

        public override float PositionY => -0.4f;
    }
}
