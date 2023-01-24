using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class PhysicsShield : Scripts.Coin.Body.Guard.Core
    {
        public PhysicsShield(string coinName, int addValue, string guardPrefabName)
            : base(coinName)
        {
            Effects = new [] { new Effect.AppendProtectionByTag(Defines.CoinTag.物理, addValue) };
            this.guardPrefabName = guardPrefabName;
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationShield();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; }

        readonly string guardPrefabName;

        public override IEnumerable<string> GetPrefabNames()
        {
            yield return guardPrefabName;
        }
    }
}
