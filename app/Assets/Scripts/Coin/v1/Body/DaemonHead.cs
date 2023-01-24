using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class DaemonHead : Scripts.Coin.Body.SetAttack.Core
    {
        public DaemonHead(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.SetAttack.SetAttackAnimationThrow("CurseA", Defines.SoundEffect.DamageFire);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[]
        {
            new Effect.CallCoinName(Defines.CoinPosition.Stock, Main.DaemonBody.name, false),
            new Effect.DamageToChangeCondition(
                false,
                Duel.PlayerCondition.PlayerConditionDetailCurse.CreatePlayerCondition(2),
                Defines.ParticleType.Curse)
        };

        public override bool IsAppendPut => true;
    }
}
