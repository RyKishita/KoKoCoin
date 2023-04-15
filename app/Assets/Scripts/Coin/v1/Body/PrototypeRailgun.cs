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
    class PrototypeRailgun : Scripts.Coin.Body.DirectAttack.Core
    {
        public PrototypeRailgun(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarShot("RailgunA", 0.5f, Defines.SoundEffect.DirectAttackShotLaser);
        }

        public override Scripts.Coin.Body.DirectAttack.IRange Range { get; } = new Scripts.Coin.Body.DirectAttack.RangeAny(7, 8, 9, 10, 11, 12, 13, 14);

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableCondition(
                true,
                PlayerConditionDetailElectric.CreatePlayerCondition(5)),
            new Effect.UseToChangeCondition(
                true,
                PlayerConditionDetailElectric.CreatePlayerCondition(-5),
                Defines.ParticleType.Electric)
        };
    }
}
