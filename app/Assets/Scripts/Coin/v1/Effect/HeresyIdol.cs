using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class HeresyIdol : Assets.Scripts.Coin.Effect.Core
    {
        public HeresyIdol()
        {
            Explain = GetEffectLocalizedString(nameof(HeresyIdol));
        }

        public override string Explain { get; }
    }
}
