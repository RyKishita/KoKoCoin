using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class CurseSword : Coin
    {
        public const string name = "v1.CurseSword";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.CurseSword(name) };

        public override string PrefabName { get { return "CurseSword"; } }

        public override float RotateY => 270f;
    }
}
