using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class CurseMusicBox : Coin
    {
        public const string name = "v1.CurseMusicBox";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.CurseMusicBox(name) };

        public override string PrefabName { get { return "CurseMusicBox"; } }

        public override float PositionY => -.45f;
        public override float RotateY => 165;
        public override float ScaleCoinValue => 0.8f;
    }
}
