using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableSetOnlyOne : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableSetOnlyOne()
        {

        }

        public override string Explain => GetEffectLocalizedString(nameof(CheckUsableSetOnlyOne));

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                if (duelData.FieldData
                        .GetAllAreaCoins()
                        .Any(scc => scc.CoinData.CoinName == selectedCoinData.CoinData.CoinName)) return false;
            }
            return true;
        }

        public override bool IsOnAreaEffect()
        {
            return false;
        }
    }
}
