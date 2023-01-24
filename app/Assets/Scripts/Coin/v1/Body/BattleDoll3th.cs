using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class BattleDoll3th : Scripts.Coin.Body.SetAttack.Core
    {
        public BattleDoll3th(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableCurrentAreaCoinName(Main.BattleDoll2th.name, 1)
        };
    }
}
