using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class UndeadCurse : Coin
    {
        public const string name = "v1.UndeadCurse";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.UndeadCurse(name) };

        public override string PrefabName { get { return "UndeadCurse"; } }

        public override float PositionY { get; } = -0.3f;
    }
}
