using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByCoinNameExist : AppendValueByCount
    {
        public AppendValueByCoinNameExist(bool bCoinOwner, Defines.CoinPosition coinPosition, string coinName, int value)
            : base(1, value)
        {
            if (coinPosition == Defines.CoinPosition.Field) throw new ArgumentException("AppendValueByCoinNameExistInField を使う");
            if (!bCoinOwner && (coinPosition == Defines.CoinPosition.Stock || coinPosition == Defines.CoinPosition.Hand)) throw new ArgumentException("相手のコインを探れる指定はできない");
            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
            this.coinName = coinName;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    formatType += 2;
                }
                string coinname = CoinList.Instance.GetCoin(coinName).DisplayName;
                string coinpositionStr = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinname), coinname },
                    { "coinposition", coinpositionStr },
                    { nameof(num), Math.Abs(num).ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByCoinNameExist), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition coinPosition;
        readonly string coinName;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
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
