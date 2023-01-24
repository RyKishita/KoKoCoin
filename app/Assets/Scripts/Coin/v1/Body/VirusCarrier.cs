using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class VirusCarrier : Scripts.Coin.Body.SetAttack.Core
    {
        public VirusCarrier(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.DamageToAddConditionTurn(
                nameof(Duel.PlayerCondition.PlayerConditionDetailVirus),
                3,
                Defines.ParticleType.Virus)
        };
    }
}
