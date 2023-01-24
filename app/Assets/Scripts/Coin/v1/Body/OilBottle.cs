using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Assets.Scripts.Duel.PlayerCondition;

namespace Assets.Scripts.Coin.v1.Body
{
    class OilBottle : Scripts.Coin.Body.DirectAttack.Core
    {
        public OilBottle(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationNearFallen("OilBottleA", Defines.SoundEffect.DirectAttackOilBottle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CheckUsableCondition(false, PlayerConditionDetailFire.CreatePlayerCondition(1))
        };
    }
}
