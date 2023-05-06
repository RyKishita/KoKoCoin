using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class HeresyIdol : Scripts.Coin.Body.Set.Core
    {
        public HeresyIdol(string coinName)
            : base(coinName)
        {
            Effects = new[] { new Effect.HeresyIdol() };
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
