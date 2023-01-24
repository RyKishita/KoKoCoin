using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Effect
{
    public interface IEffectMovable : IEffect
    {
        bool IsMovableArea(DuelData duelData, SelectedCoinData selectedCoinData, int areaNo);
    }
}
