using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ChildcareAnt : Scripts.Coin.Body.SetAttack.Core
    {
        public ChildcareAnt(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationTackle(Defines.SoundEffect.SetAttackTackle);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AddValueStatusBySetCoinName(200, true, Main.QueenAnt.name),
            new Effect.AddValueStatusByMoveCoinNameFrom(-200, true, Defines.CoinPosition.Field, Main.QueenAnt.name)
        };

        public override bool IsCoexistence => true;
    }
}
