using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ScaleArmor : Coin
    {
        public const string name = "v1.ScaleArmor";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ScaleArmor(name) };

        public override string PrefabName { get { return "ScaleArmor"; } }

        public override float PositionY => -.3f;
    }
}
