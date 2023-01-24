using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.v1.Body
{
    class CoinBodyGuardFullReflectionTurn : Scripts.Coin.Body.Guard.Core
    {
        public CoinBodyGuardFullReflectionTurn(string coinName, int turn)
            : base(coinName)
        {
            Effects = new Assets.Scripts.Coin.Effect.IEffect[] {
                new Effect.TurnToFullProtection(turn),
                new Effect.GuardToAllReflection()
            };
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
