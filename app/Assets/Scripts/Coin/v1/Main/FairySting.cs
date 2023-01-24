using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class FairySting : Coin
    {
        public const string name = "v1.FairySting";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.FairySting(name) };

        public override string PrefabName { get { return "FairySting"; } }

        public override float PositionY => -.2f;

        public override float ScaleCoinValue { get; } = 2f;
    }
}
