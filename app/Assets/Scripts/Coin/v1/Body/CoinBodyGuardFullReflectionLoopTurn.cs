using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class CoinBodyGuardFullReflectionLoopTurn : Scripts.Coin.Body.Guard.Core
    {
        public CoinBodyGuardFullReflectionLoopTurn(string coinName, int turn)
            : base(coinName)
        {
            Effects = new Assets.Scripts.Coin.Effect.IEffect[] {
                new Effect.LoopTurnToFullProtection(turn),
                new Effect.GuardToAllReflection()
            };
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
