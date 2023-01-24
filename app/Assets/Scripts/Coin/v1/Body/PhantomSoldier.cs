using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class PhantomSoldier : Scripts.Coin.Body.SetAttack.Core
    {
        public PhantomSoldier(string coinName)
            : base(coinName)
        {
            Effects = new [] { new Effect.CallCoinName( Defines.CoinPosition.Trash, coinName, true) };
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
