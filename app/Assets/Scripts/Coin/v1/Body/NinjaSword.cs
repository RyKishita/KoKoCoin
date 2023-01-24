using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class NinjaSword : Scripts.Coin.Body.DirectAttack.Core
    {
        public NinjaSword(string coinName, string setCoinName, int appendDamage)
            : base(coinName)
        {
            Effects = new[] {
                new Effect.AppendValueByCoinNameExistInField(true, setCoinName, appendDamage)
            };

            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearSlash();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
