using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Assets.Scripts.Duel.PlayerCondition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class ElectricBurst : Scripts.Coin.Body.DirectAttack.Core
    {
        public ElectricBurst(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarThrow("ElectricBurstA", 1f, Defines.SoundEffect.DamageElectric);
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableCondition(true, PlayerConditionDetailElectric.CreatePlayerCondition(1)),
            new Effect.AppendValuesByPlayerCondition(true, nameof(PlayerConditionDetailElectric), 150),
            new Effect.UseToAllLostCondition(true, nameof(PlayerConditionDetailElectric)),
        };

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;

            // 帯電ステータスがないとダメージゼロなので確認
            if (!duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo]
                    .ConditionList
                    .Has<PlayerConditionDetailElectric>()) return false;

            return true;
        }
    }
}
