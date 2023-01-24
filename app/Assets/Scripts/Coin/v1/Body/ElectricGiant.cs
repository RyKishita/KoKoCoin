using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Assets.Scripts.Duel.PlayerCondition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class ElectricGiant : Scripts.Coin.Body.SetAttack.Core
    {
        public ElectricGiant(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableCondition(
                true,
                PlayerConditionDetailElectric.CreatePlayerCondition(2)),
            new Effect.UseToChangeCondition(
                true,
                PlayerConditionDetailElectric.CreatePlayerCondition(-2),
                Defines.ParticleType.Electric)
        };
    }
}
