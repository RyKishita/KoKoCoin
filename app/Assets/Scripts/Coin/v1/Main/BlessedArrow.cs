using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BlessedArrow : Coin
    {
        public const string name = "v1.BlessedArrow";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.BlessedArrow(name) };

        public override string PrefabName { get { return "BlessedArrow"; } }

        public override float RotateY => 90f;
    }
}
