using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class IronSword:  Coin
    {
        public const string name = "v1.IronSword";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.IronSword(name) };

        public override string PrefabName { get { return "IronSword"; } }

        public override float PositionX => -0.3f;

        public override float PositionY => -0.2f;

        public override float RotateX => 315f;

        public override float RotateY => 270f;
    }
}
