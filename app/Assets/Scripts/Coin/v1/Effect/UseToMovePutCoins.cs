using Assets.Scripts.Duel;
using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToMovePutCoins : UseToMove
    {
        public UseToMovePutCoins(Defines.CoinPosition dstCoinPosition, int num)
            : base(dstCoinPosition)
        {
            if (num == 0) throw new ArgumentException(nameof(num));
            this.num = num;
        }

        readonly int num;

        public override string Explain
        {
            get
            {
                int formatType;
                if (0 < num)
                {
                    if (num == 1)
                    {
                        formatType = 0;
                    }
                    else
                    {
                        formatType = 1;
                    }
                }
                else if (num < 0)
                {
                    formatType = 2;
                }
                else
                {
                    throw new NotImplementedException();
                }

                string setcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);
                string coinposition = Defines.GetLocalizedString(dstCoinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinposition), coinposition },
                    { nameof(setcoin), setcoin},
                    { nameof(num), Math.Abs(num).ToString() },
                };

                return GetEffectLocalizedString(nameof(UseToMovePutCoins), formatType, param);
            }
        }

        protected override IEnumerable<int> GetTargetCoins(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            foreach (var area in duelData.FieldData.GetAreas())
            {
                var coins = area.GetCoinsByOwner(playerNo);
                if (coins.Count < num) continue;

                foreach (var coinID in coins.Select(coin => coin.CoinData.ID))
                {
                    yield return coinID;
                }
            }
        }
    }
}
