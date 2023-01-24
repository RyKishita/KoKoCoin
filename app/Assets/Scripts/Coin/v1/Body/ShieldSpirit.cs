using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ShieldSpirit : Scripts.Coin.Body.Guard.Core
    {
        public ShieldSpirit(string coinName, string guardPrefabName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationSwap(guardPrefabName);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.AppendValuesByCoinType(true, Defines.CoinPosition.Trash, Defines.CoinType.Guard, 400)
        };
    }
}
