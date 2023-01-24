using Assets.Scripts.Duel;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Coin.v1.Body
{
    class PlantParty : Scripts.Coin.Body.DirectAttack.Core
    {
        public PlantParty(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.DirectAttack.DirectAttackAnimationFarPushUps("PlantPartyA", 2f, 20, 5f);
        }

        public override Scripts.Coin.Body.DirectAttack.IRange Range { get; } = new Scripts.Coin.Body.DirectAttack.RangeAll();

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AppendValuesByCoinTagInField(true, Defines.CoinTag.植物, 200)
        };

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;

            // 植物の設置コインがないとダメージゼロなので確認
            if (!duelData.FieldData
                    .GetAllAreaCoins()
                    .Where(scd => scd.CoinData.OwnerPlayerNo == selectedCoinData.CoinData.OwnerPlayerNo)
                    .Any(scd => scd.GetCoinTag().HasFlag(Defines.CoinTag.植物))) return false;

            return true;
        }

    }
}
