using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class KingSword : Coin
    {
        public const string name = "v1.KingSword";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.KingSword(name) };

        public override string PrefabName { get { return "KingSword"; } }

        public override float RotateY => 270f;
    }
}
