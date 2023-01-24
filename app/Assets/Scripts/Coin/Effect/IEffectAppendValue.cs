using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Effect
{
    public interface IEffectAppendValue : IEffect
    {
        int GetAppendValue(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue);
    }
}
