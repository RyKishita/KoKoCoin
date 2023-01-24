using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.v1.Body
{
    class DragonUndead : Scripts.Coin.Body.SetAttack.Core
    {
        public DragonUndead(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CheckUsableCoinName(true, Defines.CoinPosition.Trash, Main.Dragon.name, 1)
        };
    }
}
