using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class Ninja : Scripts.Coin.Body.SetAttack.Core
    {
        public Ninja(string coinName, int num, int addDamage)
            : base(coinName)
        {
            var effect = (num == 1)
                    ? (Scripts.Coin.Effect.IEffect)new Effect.AppendValueByCoinNameExistInField(true, coinName, addDamage)
                    : (Scripts.Coin.Effect.IEffect)new Effect.AppendValueByCoinNameOverInField(true, coinName, num, addDamage);

            Effects = new [] { effect };

            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }

        public override bool IsAppendPut => true;
    }
}
