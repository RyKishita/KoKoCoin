using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Effect
{
    public interface IEffectGlobalAppendValue : IEffect
    {
        int GetGlobalAppendValue(DuelData duelData, SelectedCoinData targetSelectedCoinData, SelectedCoinData thisSelectedCoinData, int baseValue);
    }
}
