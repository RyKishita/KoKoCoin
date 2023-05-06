using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class DimensionBeacon : Assets.Scripts.Coin.Effect.Core
    {
        public DimensionBeacon()
        {
            Explain = GetEffectLocalizedString(nameof(DimensionBeacon));
        }

        public override string Explain { get; }
    }
}
