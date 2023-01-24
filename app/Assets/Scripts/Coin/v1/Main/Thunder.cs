using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Thunder : Coin
    {
        public const string name = "v1.Thunder";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.Thunder(name) };

        public override string PrefabName { get { return "Thunder"; } }

        //public override float PositionY => -.5f;

        public override float RotateZ => 330f;
    }
}
