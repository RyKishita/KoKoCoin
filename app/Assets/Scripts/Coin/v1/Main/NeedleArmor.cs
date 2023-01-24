using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class NeedleArmor : Coin
    {
        public const string name = "v1.NeedleArmor";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.NeedleArmor(name) };

        public override string PrefabName { get { return "NeedleArmor"; } }

        public override float PositionY => -.4f;
    }
}
