using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class MagiciansStrawA : Scripts.Coin.Body.SetAttack.Core
    {
        public MagiciansStrawA(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.SubstitutionMoveCoinName(Main.ArtfulMagician.name),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailSkipGuard.CreatePlayerCondition(3),
                Defines.ParticleType.Anesthetize)
        };
    }
}
