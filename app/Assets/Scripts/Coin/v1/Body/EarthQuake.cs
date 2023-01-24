using Assets.Scripts.Duel;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class EarthQuake : Scripts.Coin.Body.DirectAttack.Core
    {
        public EarthQuake(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationQuake();
        }

        public override Scripts.Coin.Body.DirectAttack.IRange Range { get; } = new Scripts.Coin.Body.DirectAttack.RangeAll();
    }
}
