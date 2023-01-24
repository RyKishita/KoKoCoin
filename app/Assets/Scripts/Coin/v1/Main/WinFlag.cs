using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class WinFlag : Coin
    {
        public const string name = "v1.WinFlag";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.WinFlag(name) };

        public override string PrefabName { get { return "WinFlag"; } }

        public override float PositionX { get; } = -0.15f;
    }
}
