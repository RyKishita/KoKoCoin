using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValuesByCoinTotalSize : AppendValuesByCount
    {
        public AppendValuesByCoinTotalSize(bool bCoinOwner, Defines.CoinPosition coinPosition, int value)
            : base(value)
        {
            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
        }

        public override string Explain
        {
            get
            {
                int format = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string coinposition = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(coinposition), coinposition },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValuesByCoinTotalSize), format, param);
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
                        .Select(scd => scd.CoinData)
                        .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                        .Where(cd => bCoinOwner == (cd.OwnerPlayerNo == playerNo))
                        .Sum(cd => cd.GetCoin().Size);
            }
            else
            {
                if (bCoinOwner)
                {
                    return duelData.Players[playerNo]
                        .GetCoinDataList(coinPosition)
                        .Items
                        .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                        .Sum(cd => cd.GetCoin().Size);
                }
                else
                {
                    return duelData.Players
                        .Where(player => player.PlayerNo != playerNo)
                        .SelectMany(player => player.GetCoinDataList(coinPosition).Items)
                        .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                        .Sum(cd => cd.GetCoin().Size);
                }
            }
        }
    }
}
