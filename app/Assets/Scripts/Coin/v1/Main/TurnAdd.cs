using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class TurnAdd : Coin
    {
        public const string name = "v1.TurnAdd";

        public override string Name => name;

        public override string PrefabName => "TurnAdd";

        public override Scripts.Coin.Body.Core[] Bodies => new[] { new Body.TurnAdd(name, 1) };

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .9f;
    }
}
