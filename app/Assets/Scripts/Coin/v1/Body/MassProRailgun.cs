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
    class MassProRailgun : Scripts.Coin.Body.DirectAttack.Core
    {
        public MassProRailgun(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarShot("RailgunA", 0.5f, Defines.SoundEffect.DirectAttackShotLaser);
        }

        public override Scripts.Coin.Body.DirectAttack.IRange Range { get; } = new Scripts.Coin.Body.DirectAttack.RangeAny(6, 7, 8, 9, 10, 11);

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects => new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableCondition(
                true,
                PlayerConditionDetailElectric.CreatePlayerCondition(3)),
            new Effect.UseToChangeCondition(
                true,
                PlayerConditionDetailElectric.CreatePlayerCondition(-3),
                Defines.ParticleType.Electric)
        };
    }
}
