using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class SlingShot : Coin
    {
        public const string name = "v1.SlingShot";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new Body.SlingShot[] { new Body.SlingShot(name) };

        public override string PrefabName { get { return "SlingShot"; } }
    }
}
