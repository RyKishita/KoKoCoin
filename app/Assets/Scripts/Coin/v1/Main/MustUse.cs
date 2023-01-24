using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MustUse : Coin
    {
        public const string name = "v1.MustUse";

        public override string Name => name;

        public override string PrefabName => "MustUse";

        public override Scripts.Coin.Body.Core[] Bodies => new[] { new Body.MustUse(name) };

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .9f;
    }
}
