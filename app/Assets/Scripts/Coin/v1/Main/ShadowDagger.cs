using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ShadowDagger : Coin, IAdditionalShow
    {
        public const string name = "v1.ShadowDagger";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ShadowDagger(name) };

        public override string PrefabName { get { return "ShadowDagger"; } }

        public override float PositionY => -0.1f;

        public override float RotateX => 45f;

        public override float RotateY => 270f;

        public string[] AdditionalShowCoinNames => new[] { ShadowSword.name, ShadowLongSword.name, ShadowClaymore.name };
    }
}
