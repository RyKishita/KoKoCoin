using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Confusion : Scripts.Coin.Body.Guard.Core
    {
        public Confusion(string coinName, int confusionValue, string guardPrefabName)
            : base(coinName)
        {
            Effects = new []
            {
                new Effect.GuardToChangeCondition(
                    false,
                    Duel.PlayerCondition.PlayerConditionDetailConfusionMove.CreatePlayerCondition(confusionValue),
                    Defines.ParticleType.ConfusionMove)
            };
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationSwap(guardPrefabName);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
