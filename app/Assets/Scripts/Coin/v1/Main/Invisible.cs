using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Invisible : Coin
    {
        public const string name = "v1.Invisible";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.Invisible(name, "Invisible") };

        public override string PrefabName { get { return "Invisible"; } }
    }
}
