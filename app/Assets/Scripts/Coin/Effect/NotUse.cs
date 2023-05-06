using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.Effect
{
    class NotUse : Core
    {
        public NotUse()
        {
            Explain = GetEffectLocalizedString(nameof(NotUse));
        }

        public override string Explain { get; }
    }
}
