using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Coin.v1.Body
{
    class StoneOfPoor : Scripts.Coin.Body.DirectAttack.Core
    {
        public StoneOfPoor(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarThrowsRandom("StoneOfPoorA", 10, 5f);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] { new Effect.AppendValueByCurrentArea(true, Defines.AreaType.D, 400) };
    }
}
