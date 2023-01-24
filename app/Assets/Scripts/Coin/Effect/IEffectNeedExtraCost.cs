using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Effect
{
    interface IEffectNeedExtraCost : IEffect
    {
        int Value { get; }

        bool IsValidExtraCost(List<Duel.CoinData> coinDatas);
    }
}
