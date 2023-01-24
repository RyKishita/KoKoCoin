using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using UnityEngine;

namespace Assets.Scripts.Coin.v1.Body
{
    class ShotSpirit : Scripts.Coin.Body.DirectAttack.Core
    {
        public ShotSpirit(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarBulles("ShotSpiritA", 0.2f, Defines.SoundEffect.DirectAttackShotSpirit);
        }

        public override Scripts.Coin.Body.DirectAttack.IRange Range { get; } = new Scripts.Coin.Body.DirectAttack.RangeAll();

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.CheckUsableCoin(true, Defines.CoinPosition.Trash, Defines.CoinType.DirectAttack, 1),
            new Effect.AppendValuesByCoinType(true, Defines.CoinPosition.Trash, Defines.CoinType.DirectAttack, 100)
        };

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;

            // IsNoNeed だと直接攻撃コインの捨てコインが無くてもダメージゼロで使ってしまうので確認
            return duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo]
                    .GetCoinDataList(Defines.CoinPosition.Trash)
                    .Items
                    .Where(cd => cd.GetCoin().IsExistFromBodies<Scripts.Coin.Body.DirectAttack.Core>())
                    .Any();
        }
    }
}
