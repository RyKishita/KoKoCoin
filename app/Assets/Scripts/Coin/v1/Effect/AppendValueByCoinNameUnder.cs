using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByCoinNameUnder : AppendValueByCount
    {
        public AppendValueByCoinNameUnder(bool bCoinOwner, Defines.CoinPosition coinPosition, string coinName, int num, int value)
            : base(-num, value)
        {
            if (num <= 0) throw new ArgumentOutOfRangeException(nameof(num));

            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
            this.coinName = coinName;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string coinname = CoinList.Instance.GetCoin(coinName).DisplayName;
                string coinpositionStr = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(coinname), coinname },
                    { "coinposition", coinpositionStr },
                    { nameof(num), Math.Abs(num).ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByCoinNameUnder), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition coinPosition;
        readonly string coinName;

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
                        .Where(cd => cd.CoinName == coinName)
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
                        .Where(cd => cd.CoinName == coinName)
                        .Count();
                }
                else
                {
                    return duelData.Players
                        .Where(player => player.PlayerNo != playerNo)
                        .SelectMany(player => player.GetCoinDataList(coinPosition).Items)
                        .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                        .Where(cd => cd.CoinName == coinName)
                        .Count();
                }
            }
        }
    }
}
