using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    /// <summary>
    /// 単語の意味は空からの釈放。なので逆に地上への束縛なので重力
    /// </summary>
    class SkyAbsolver : Coin
    {
        public const string name = "v1.SkyAbsolver";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[] { new Body.SkyAbsolver(name) };

        public override string PrefabName { get { return "SkyAbsolver"; } }

        public override float PositionY => 0.05f;
    }
}
