using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ConditionCount : Coin
    {
        public const string name = "v1.ConditionCount";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.ConditionCount(name, -2) };

        public override string PrefabName => "ConditionCount";

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .9f;
    }
}
