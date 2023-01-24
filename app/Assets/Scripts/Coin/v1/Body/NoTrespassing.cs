using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class NoTrespassing : Scripts.Coin.Body.Set.Core
    {
        public NoTrespassing(string coinName)
            : base(coinName)
        {

        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.DontStop(),
            new Effect.TurnToMove(Defines.CoinPosition.Field, 10, Defines.CoinPosition.Trash),
        };
    }
}
