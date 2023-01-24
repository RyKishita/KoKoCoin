using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class MagiciansStrawP : Coin
    {
        public const string name = "v1.MagiciansStrawP";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.MagiciansStrawP(name) };

        public override string PrefabName { get { return "MagiciansStrawP"; } }
    }
}
