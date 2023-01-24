using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Assets.Scripts.Duel.PlayerCondition;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ElectricKing : Scripts.Coin.Body.SetAttack.Core
    {
        public ElectricKing(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("ThunderA", Defines.SoundEffect.DamageElectric);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableSetOnlyOne(),
            new Effect.CheckUsableCondition(
                true,
                PlayerConditionDetailElectric.CreatePlayerCondition(4)),
            new Effect.UseToChangeCondition(
                true,
                PlayerConditionDetailElectric.CreatePlayerCondition(-4),
                Defines.ParticleType.Electric)
        };
    }
}
