using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class IceArmor : Coin
    {
        public const string name = "v1.IceArmor";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.IceArmor(name) };

        public override string PrefabName { get { return "IceArmor"; } }

        public override float PositionY => -.4f;
    }
}
