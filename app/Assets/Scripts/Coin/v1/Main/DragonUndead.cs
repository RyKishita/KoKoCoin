using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class DragonUndead : Coin
    {
        public const string name = "v1.DragonUndead";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.DragonUndead(name) };

        public override string PrefabName { get { return "DragonUndead"; } }

        public override float PositionY => -.4f;

        public override float RotateY => 135f;
    }
}
