using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ArtfulMagician : Coin
    {
        public const string name = "v1.ArtfulMagician";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new [] { new Body.ArtfulMagician(name) };

        public override string PrefabName => "ArtfulMagician";
    }
}
