using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class Redraw : Coin
    {
        public const string name = "v1.Redraw";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.Redraw(name) };

        public override string PrefabName => "Redraw";
    }
}
