using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByCoinOver : AppendValueByCount
    {
        public AppendValueByCoinOver(bool bCoinOwner, Defines.CoinPosition coinPosition, int num, int value)
            : base(num, value)
        {
            if (coinPosition == Defines.CoinPosition.Field) throw new ArgumentException("必要なら AppendValueByCoinOverInField を作る");
            if (!bCoinOwner && (coinPosition == Defines.CoinPosition.Stock || coinPosition == Defines.CoinPosition.Hand)) throw new ArgumentException("相手のコインを探れる指定はできない");
            if (num <= 0) throw new ArgumentOutOfRangeException(nameof(num));
            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
        }

        public override string Explain
        {
            get
            {
                int format = (0 < Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    format += 2;
                }
                string coinpositionStr = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { "coinposition", coinpositionStr },
                    { nameof(num), num.ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByCoinOver), format, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition coinPosition;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            if (coinPosition == Defines.CoinPosition.Field)
            {
                return duelData
                        .FieldData
                        .GetAllAreaCoins()
                        .Where(scd => scd.CoinData.ID != selectedCoinData.CoinData.ID)
                        .Where(scd => bCoinOwner == (scd.CoinData.OwnerPlayerNo == playerNo))
                        .Count();
            }
            else
            {
                if (bCoinOwner)
                {
                    return duelData.Players[playerNo]
                        .GetCoinDataList(coinPosition)
                        .Items
                        .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                        .Count();
                }
                else
                {
                    return duelData.Players
                        .Where(player => player.PlayerNo != playerNo)
                        .SelectMany(player => player.GetCoinDataList(coinPosition).Items)
                        .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                        .Count();
                }
            }
        }
    }

}