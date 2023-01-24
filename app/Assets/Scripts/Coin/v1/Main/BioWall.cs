using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BioWall : Coin
    {
        public const string name = "v1.BioWall";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.BioWall(name) };

        public override string PrefabName { get { return "BioWall"; } }

        public override float ScaleCoinValue  => .9f;
    }
}
