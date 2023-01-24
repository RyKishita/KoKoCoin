using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableCoinTag : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableCoinTag(bool bCoinOwner, Defines.CoinPosition coinPosition, Defines.CoinTag coinTag, int num)
        {
            if (coinPosition == Defines.CoinPosition.Field) throw new ArgumentException("代わりにCheckUsableCoinTagInFieldを使う");
            if (!bCoinOwner && (coinPosition == Defines.CoinPosition.Stock || coinPosition == Defines.CoinPosition.Hand)) throw new ArgumentException("相手コインを探索できる指定はダメ");

            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
            this.coinTag = coinTag;
            this.num = num;
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition coinPosition;
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

                string coinposition = Defines.GetLocalizedString(coinPosition);
                string tagname = Defines.GetLocalizedString(coinTag);

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinposition), coinposition },
                    { nameof(tagname), tagname },
                    { nameof(num), Math.Abs(num).ToString() },
                };

                return GetEffectLocalizedString(nameof(CheckUsableCoinTag), formatType, param);
            }
        }

        int GetCount(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            if (bCoinOwner)
            {
                return duelData.Players[playerNo]
                    .GetCoinDataList(coinPosition)
                    .Items
                    .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                    .Where(cd => cd.HasCoinTag(coinTag))
                    .Count();
            }
            else
            {
                return duelData.Players
                    .Where(player => player.PlayerNo != playerNo)
                    .SelectMany(player => player.GetCoinDataList(coinPosition).Items)
                    .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                    .Where(cd => cd.HasCoinTag(coinTag))
                    .Count();
            }
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

        public override bool IsProcessedOnArea()
        {
            return false;
        }
    }
}
