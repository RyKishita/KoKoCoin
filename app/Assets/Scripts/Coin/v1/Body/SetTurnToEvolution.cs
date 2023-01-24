using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class SetTurnToEvolution : Scripts.Coin.Body.Set.Core
    {
        public SetTurnToEvolution(string coinName, int turn, string copiedCoinName, bool bGreaterCoin)
            : base(coinName)
        {
            Effects = new [] {
                new Effect.TurnToEvolutionInField(turn, copiedCoinName, bGreaterCoin)
            };
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
