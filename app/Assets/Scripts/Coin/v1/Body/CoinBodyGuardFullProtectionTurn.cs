using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.v1.Body
{
    class CoinBodyGuardFullProtectionTurn : Scripts.Coin.Body.Guard.Core
    {
        public CoinBodyGuardFullProtectionTurn(string coinName, int turn)
            : base(coinName)
        {
            Effects = new[] { new Effect.TurnToFullProtection(turn) };
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
