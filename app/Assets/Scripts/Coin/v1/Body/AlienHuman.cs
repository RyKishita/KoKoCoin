using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class AlienHuman : Scripts.Coin.Body.SetAttack.Core
    {
        public AlienHuman(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new[]
        {
            new Effect.CheckUsableCoinName(true, Defines.CoinPosition.Hand, Main.DimensionBeacon.name, 1)
        };
    }
}
