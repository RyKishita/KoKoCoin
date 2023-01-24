using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.Body.DirectAttack
{
    public interface IRange
    {
        string RangeText { get; }

        IEnumerable<string> Explains { get; }

        bool IsInRange(DuelData duelData, int targetPlayerNo, SelectedCoinData selectedCoinData);
    }
}
