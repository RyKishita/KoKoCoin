using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseForce : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUseForce
    {
        public UseForce()
        {

        }

        public override string Explain => GetEffectLocalizedString(nameof(UseForce));

        public override bool IsProcessedOnArea()
        {
            return false;
        }

        public override bool IsAdvantage(Duel.DuelData duelData)
        {
            return false;
        }
    }
}
