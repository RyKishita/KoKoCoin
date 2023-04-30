using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class WindFairy : Scripts.Coin.Body.SetAttack.Core
    {
        public WindFairy(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("WindFairyA", Defines.SoundEffect.DirectAttackBow);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AppendValuesByTurn(Defines.CoinPosition.Field, 50)
        };
    }
}
