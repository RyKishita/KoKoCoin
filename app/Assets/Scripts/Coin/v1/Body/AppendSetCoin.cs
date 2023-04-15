using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.v1.Body
{
    class AppendSetCoin : Scripts.Coin.Body.SetAttack.Core
    {
        public AppendSetCoin(string coinName, Defines.CoinTag appendCoinTag, int appendDamage)
            : base(coinName)
        {
            Effects = new [] {
                new Effect.AppendValueBySameAreaTagExist(true, appendCoinTag, appendDamage)
            };
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }

        public override bool IsCoexistence => true;
    }
}
