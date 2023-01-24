using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SecondArrow : Coin
    {
        public const string name = "v1.SecondArrow";

        public SecondArrow()
        {
            Bodies = new[] { new Body.NextArrow(name, 500, PrefabName, FirstArrow.name) };
        }

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; }

        public override string PrefabName { get { return "SecondArrow"; } }

        public override float RotateY { get; } = 90f;
    }
}
