using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class DragonEgg : Coin
    {
        public const string name = "v1.DragonEgg";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.SetTurnToEvolution(name, 10, Dragon.name, true) };

        public override string PrefabName { get { return "DragonEgg"; } }
    }
}
