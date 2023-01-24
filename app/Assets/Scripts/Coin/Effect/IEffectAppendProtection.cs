using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Effect
{
    public interface IEffectAppendProtection : IEffect
    {
        int GetAppendProtection(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData directAttackSelectedCoinData);
    }
}
