using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class DuplicateArrow : Coin
    {
        public const string name = "v1.DuplicateArrow";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.DuplicateArrow(name, 2) };

        public override string PrefabName { get { return "DuplicateArrow"; } }

        public override float RotateY => 90f;
    }
}
