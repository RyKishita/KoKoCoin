using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class NextArrow : Scripts.Coin.Body.DirectAttack.Core
    {
        public NextArrow(string coinName, int appendDamage, string basePrefabName, params string[] coinNames)
            : base(coinName)
        {
            Effects = coinNames.Select(coinName => new Effect.AppendValueByCoinNameExist(true, Defines.CoinPosition.Trash, coinName, appendDamage)).ToArray();
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarBow(
                ZString.Format("{0}B", basePrefabName),
                ZString.Format("{0}A", basePrefabName));
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }
    }
}
