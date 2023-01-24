using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class IciclePic : Coin
    {
        public const string name = "v1.IciclePic";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.IciclePic(name) };

        public override string PrefabName { get { return "IciclePic"; } }

        public override float PositionY => -.2f;

        public override float ScaleCoinValue { get; } = 2f;
    }
}
