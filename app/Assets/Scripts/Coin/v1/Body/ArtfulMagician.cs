using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ArtfulMagician : Scripts.Coin.Body.SetAttack.Core
    {
        public ArtfulMagician(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("ArtfulMagicianA", Defines.SoundEffect.DirectAttackThrowMagic);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableSetOnlyOne(),
            new Effect.LoopTurnToGetCopyInField(2, Main.StoneMissile.name)
        };
    }
}
