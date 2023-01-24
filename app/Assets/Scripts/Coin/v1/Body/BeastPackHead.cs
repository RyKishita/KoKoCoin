using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class BeastPackHead : Scripts.Coin.Body.SetAttack.Core
    {
        public BeastPackHead(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CheckUsableSetOnlyOne(),
            new Effect.AddValueStatusByMoveCoinTagToField(200, true, Defines.CoinTag.獣)
        };
    }
}
