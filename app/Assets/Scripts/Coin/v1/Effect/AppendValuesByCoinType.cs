using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValuesByCoinType : AppendValuesByCount
    {
        public AppendValuesByCoinType(bool bCoinOwner, Defines.CoinPosition coinPosition, Defines.CoinType selectCoinType, int value)
            : base(value)
        {
            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
            this.selectCoinType = selectCoinType;
        }

        public override string Explain
        {
            get
            {
                int format = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string coinposition = Defines.GetLocalizedString(coinPosition);
                string cointype = Defines.GetLocalizedString(selectCoinType);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(coinposition), coinposition },
                    { nameof(cointype), cointype },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValuesByCoinType), format, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition coinPosition;
        readonly Defines.CoinType selectCoinType;

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
                        .Where(scd => scd.GetCoinBody().IsCoinType(selectCoinType))
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
                        .Where(cd => cd.HasCoinType(selectCoinType))
                        .Count();
                }
                else
                {
                    return duelData.Players
                        .Where(player => player.PlayerNo != playerNo)
                        .SelectMany(player => player.GetCoinDataList(coinPosition).Items)
                        .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                        .Where(cd => cd.HasCoinType(selectCoinType))
                        .Count();
                }
            }
        }
    }
}
