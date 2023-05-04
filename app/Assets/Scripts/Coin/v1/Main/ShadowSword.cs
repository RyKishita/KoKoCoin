using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ShadowSword : Coin, IExtraCoin
    {
        public const string name = "v1.ShadowSword";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ShadowSword(name) };

        public override string PrefabName { get { return "ShadowSword"; } }

        public override float PositionX => 0.2f;

        public override float PositionY => -0.2f;

        public override float RotateX => 45f;

        public override float RotateY => 270f;

        public bool IsShowList => true;
    }
}
