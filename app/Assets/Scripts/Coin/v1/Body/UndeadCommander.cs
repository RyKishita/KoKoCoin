using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class UndeadCommander : Scripts.Coin.Body.SetAttack.Core
    {
        public UndeadCommander(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("UndeadCommanderA", Defines.SoundEffect.DirectAttackStone);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AppendValuesByCoinTagInField(true, Defines.CoinTag.死霊, 100),
            new Effect.UseToMoveTagALLToStock(true, Defines.CoinPosition.Trash, Defines.CoinTag.死霊)
        };
    }
}
