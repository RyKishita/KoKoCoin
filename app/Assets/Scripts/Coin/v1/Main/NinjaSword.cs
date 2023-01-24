using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class NinjaSword : Coin
    {
        public const string name = "v1.NinjaSword";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.NinjaSword(name,  Ninja.name, 200) };

        public override string PrefabName { get { return "NinjaSword"; } }

        public override float RotateY => 270f;
    }
}
