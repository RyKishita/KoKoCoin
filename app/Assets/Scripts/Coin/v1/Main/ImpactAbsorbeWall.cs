using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ImpactAbsorbeWall : Coin
    {
        public const string name = "v1.ImpactAbsorbeWall";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ImpactAbsorbeWall(name) };

        public override string PrefabName { get { return "ImpactAbsorbeWall"; } }

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .8f;
    }
}
