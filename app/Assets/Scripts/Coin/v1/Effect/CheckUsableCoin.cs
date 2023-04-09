using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableCoin : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableCoin(bool bCoinOwner, Defines.CoinPosition coinPosition, Defines.CoinType coinType, int num)
        {
            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
            this.coinType = coinType;
            this.num = num;
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition coinPosition;
        readonly Defines.CoinType coinType;
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

                if (coinType != Defines.CoinType.None)
                {
                    formatType += 4;
                }

                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string coinposition = Defines.GetLocalizedString(coinPosition);
                string cointype = (coinType == Defines.CoinType.None) ? "" : Defines.GetLocalizedString(coinType);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(coinposition), coinposition },
                    { nameof(cointype), cointype },
                    { nameof(num), Math.Abs(num).ToString() },
                };

                return GetEffectLocalizedString(nameof(CheckUsableCoin), formatType, param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                IEnumerable<Defines.CoinType> coinTypes = GetCoinTypes(duelData, selectedCoinData);

                if (coinType != Defines.CoinType.None)
                {
                    coinTypes = coinTypes.Where(ct => ct.HasFlag(coinType));
                }

                int count = coinTypes.Count();

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

        private IEnumerable<Defines.CoinType> GetCoinTypes(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            if (coinPosition == Defines.CoinPosition.Field)
            {
                return duelData
                        .FieldData
                        .GetAllAreaCoins()
                        .Where(scd => scd.CoinData.ID != selectedCoinData.CoinData.ID)
                        .Where(scc => bCoinOwner == (scc.CoinData.OwnerPlayerNo == playerNo))
                        .Select(scd => scd.GetCoinBody().CoinType);
            }
            else
            {
                if (bCoinOwner)
                {
                    return duelData.Players[playerNo]
                        .GetCoinDataList(coinPosition)
                        .Items
                        .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                        .SelectMany(cd => cd.GetCoin().Bodies.Select(body => body.CoinType));
                }
                else
                {
                    return duelData.Players
                        .Where(player => player.PlayerNo != playerNo)
                        .SelectMany(player => player.GetCoinDataList(coinPosition).Items)
                        .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                        .SelectMany(cd => cd.GetCoin().Bodies.Select(body => body.CoinType));
                }
            }
        }

        public override bool IsOnAreaEffect()
        {
            return false;
        }
    }
}
