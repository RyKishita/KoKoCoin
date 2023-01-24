using Assets.Scripts.Duel.PlayerCondition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class GuardBooster : Scripts.Coin.Body.Set.Core
    {
        public GuardBooster(string coinName, int value)
            : base(coinName)
        {
            usePlayerCondition = PlayerConditionDetailAppendGuard.CreatePlayerCondition(value);
            Effects = new[] { new Effect.AddConditionToPlayerBySet(usePlayerCondition, 0 < value ? Defines.ParticleType.Buf : Defines.ParticleType.Debuf) };
        }

        public override Scripts.Coin.Effect.IEffect[] Effects { get; }

        readonly PlayerCondition usePlayerCondition;
    }
}
