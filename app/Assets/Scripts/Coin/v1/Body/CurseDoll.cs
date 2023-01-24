using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.v1.Body
{
    class CurseDoll : Scripts.Coin.Body.SetAttack.Core
    {
        public CurseDoll(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("CurseA", Defines.SoundEffect.DirectAttackThrowMagic);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CheckUsableTurn(2),
            new Effect.DamageToAddConditionTurn(nameof(Duel.PlayerCondition.PlayerConditionDetailCurse), 4, Defines.ParticleType.Curse)
        };
    }
}
