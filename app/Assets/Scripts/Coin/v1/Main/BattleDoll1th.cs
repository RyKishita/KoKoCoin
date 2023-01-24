using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class BattleDoll1th : Coin
    {
        public const string name = "v1.BattleDoll1th";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.BattleDoll1th(name) };

        public override string PrefabName { get { return "BattleDoll1th"; } }

        public override float RotateY => 195f;
        public override float ScaleCoinValue => 0.9f;
    }
}
