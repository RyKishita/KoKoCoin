using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Effect
{
    public interface IEffectUsable : IEffect
    {
        bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData);
    }
}
