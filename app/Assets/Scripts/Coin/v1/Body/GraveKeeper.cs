using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class GraveKeeper : Scripts.Coin.Body.SetAttack.Core
    {
        public GraveKeeper(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("GraveKeeperA", Defines.SoundEffect.DirectAttackStone);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CheckUsableCoinTag(true, Defines.CoinPosition.Trash, Defines.CoinTag.人間, 1)
        };
    }
}
