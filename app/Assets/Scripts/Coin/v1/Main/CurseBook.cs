using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class CurseBook : Coin
    {
        public const string name = "v1.CurseBook";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.CurseBook(name) };

        public override string PrefabName { get { return "CurseBook"; } }

        public override float RotateX => 10f;
        public override float RotateY => 210f;
        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .7f;
    }
}
