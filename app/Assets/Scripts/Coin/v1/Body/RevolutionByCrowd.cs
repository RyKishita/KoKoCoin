using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class RevolutionByCrowd : Scripts.Coin.Body.Set.Core
    {
        public RevolutionByCrowd(string coinName)
            : base(coinName)
        {

        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] { new Effect.SetAllAreaTypeToWin() };
    }
}
