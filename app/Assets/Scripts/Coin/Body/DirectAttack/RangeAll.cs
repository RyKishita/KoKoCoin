using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.Body.DirectAttack
{
    class RangeAll : IRange
    {
        public string RangeText => Messages.GetLocalizedString(Messages.MessageType.RangeALL_short);

        public IEnumerable<string> Explains
        {
            get
            {
                yield return Messages.GetLocalizedString(Messages.MessageType.RangeALL);
            }
        }

        public bool IsInRange(DuelData duelData, int targetPlayerNo, SelectedCoinData selectedCoinData)
        {
            return true;
        }
    }
}
