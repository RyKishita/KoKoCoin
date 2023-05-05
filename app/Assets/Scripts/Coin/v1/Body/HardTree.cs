using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class HardTree : Scripts.Coin.Body.SetAttack.Core
    {
        public HardTree(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("RodA", Defines.SoundEffect.DirectAttackStone);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AddValueStatusBySwapSetName(500, Main.Manure.name)
        };
    }
}
