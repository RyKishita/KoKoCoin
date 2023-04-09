using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableCoinTagInField : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableCoinTagInField(bool bCoinOwner, Defines.CoinTag coinTag, int num)
        {
            this.bCoinOwner = bCoinOwner;
            this.coinTag = coinTag;
            this.num = num;
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinTag coinTag;
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

                string tagname = Defines.GetLocalizedString(coinTag);

                var param = new Dictionary<string, string>()
                {
                    { nameof(tagname), tagname },
                    { nameof(num), Math.Abs(num).ToString() },
                };

                return GetEffectLocalizedString(nameof(CheckUsableCoinTagInField), formatType, param);
            }
        }

        int GetCount(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            return duelData
                    .FieldData
                    .GetAllAreaCoins()
                    .Where(scd => scd.CoinData.ID != selectedCoinData.CoinData.ID)
                    .Where(scd => bCoinOwner == (scd.CoinData.OwnerPlayerNo == playerNo))
                    .Where(scd => scd.GetCoinTag().HasFlag(coinTag))
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
