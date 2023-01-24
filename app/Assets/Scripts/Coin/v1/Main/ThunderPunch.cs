using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ThunderPunch : Coin
    {
        public const string name = "v1.ThunderPunch";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ThunderPunch(name) };

        public override string PrefabName { get { return "ThunderPunch"; } }

        public override float PositionY => -.4f;
    }
}
