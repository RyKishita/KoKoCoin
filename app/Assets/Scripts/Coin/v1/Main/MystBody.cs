using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MystBody : Coin
    {
        public const string name = "v1.MystBody";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.MystBody(name, "MystBody") };

        public override string PrefabName { get { return "MystBody"; } }
    }
}
