using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableCoinNameInField : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableCoinNameInField(bool bCoinOwner, string coinName, int num)
        {
            this.bCoinOwner = bCoinOwner;
            this.coinName = coinName;
            this.num = num;
        }

        readonly bool bCoinOwner;
        readonly string coinName;
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
                    formatType = 3;
                }

                if (!bCoinOwner)
                {
                    formatType += 4;
                }

                string coinname = CoinList.Instance.GetCoin(coinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinname), coinname },
                    { nameof(num), Math.Abs(num).ToString() },
                };

                return GetEffectLocalizedString(nameof(CheckUsableCoinNameInField), formatType, param);
            }
        }

        int GetCount(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            return duelData
                    .FieldData
                    .GetAllAreaCoins()
                    .Select(scd => scd.CoinData)
                    .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                    .Where(cd => bCoinOwner == (cd.OwnerPlayerNo == playerNo))
                    .Where(cd => cd.CoinName == coinName)
                    .Count();
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                int count = GetCount(duelData, selectedCoinData);

                if (0 < num)
                {
                    if (count < num) return false;
                }
                else if (num < 0)
                {
                    if (Math.Abs(num) < count) return false;
                }
                else
                {
                    if (count != 0) return false;
                }
            }
            return true;
        }

        public override bool IsOnAreaEffect()
        {
            return false;
        }
    }
}
