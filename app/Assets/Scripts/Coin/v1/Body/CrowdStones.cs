using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Coin.v1.Body
{
    class CrowdStones : Scripts.Coin.Body.DirectAttack.Core
    {
        public CrowdStones(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarThrowsContinue("CrowdStonesA", 5);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AppendValuesByCoinTagInField(true, Defines.CoinTag.人間, 100)
        };
    }
}
