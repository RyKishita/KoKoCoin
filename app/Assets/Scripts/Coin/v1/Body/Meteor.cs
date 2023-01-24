using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.v1.Body
{
    class Meteor : Scripts.Coin.Body.DirectAttack.Core
    {
        public Meteor(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarFallen("Meteor", Defines.SoundEffect.DirectAttackThrowMeteor);
        }

        public override Scripts.Coin.Body.DirectAttack.IRange Range { get; } = new Scripts.Coin.Body.DirectAttack.RangeAll();

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableNeedExtraCostByNum(1)
        };
    }
}
