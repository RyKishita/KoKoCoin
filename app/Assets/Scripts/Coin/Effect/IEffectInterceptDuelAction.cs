using Assets.Scripts.Duel;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Effect
{
    public interface IEffectInterceptDuelAction : IEffect
    {
        bool InterceptDuelAction(DuelManager duelManager, SelectedCoinData selectedCoinData, Duel.DuelEvent.Action duelEventAction);
    }
}
