using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableCoinTotalSizeOver : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bCoinOwner">true:コイン所有者 false:コイン所有者の対戦相手 null:全て</param>
        /// <param name="coinPosition"></param>
        /// <param name="size"></param>
        public CheckUsableCoinTotalSizeOver(bool? bCoinOwner, Defines.CoinPosition coinPosition, int size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
            this.size = size;
        }

        readonly bool? bCoinOwner;
        readonly Defines.CoinPosition coinPosition;
        readonly int size;

        public override string Explain
        {
            get
            {
                int formatType = bCoinOwner.HasValue ? 0 : 1;

                string player = bCoinOwner.HasValue ? Defines.GetLocalizedString(bCoinOwner.Value ? Defines.StringEnum.You : Defines.StringEnum.Opponent) : "";
                string coinposition = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(coinposition), coinposition },
                    { "num", size.ToString() },
                };

                return GetEffectLocalizedString(nameof(CheckUsableCoinTotalSizeOver), formatType, param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                int totalSize;

                if (bCoinOwner.HasValue)
                {
                    var playerNos = bCoinOwner.Value
                        ? new List<int>() { playerNo }
                        : duelData.GetOtherTeamPlayerNos(playerNo).ToList();

                    if (coinPosition == Defines.CoinPosition.Field)
                    {
                        totalSize = duelData.FieldData
                                        .GetAllAreaCoins()
                                        .Where(scd => playerNos.Contains(scd.CoinData.OwnerPlayerNo))
                                        .Sum(scd => scd.GetCoin().Size);
                    }
                    else
                    {
                        totalSize = playerNos.Sum(playerNo => duelData.Players[playerNo]
                                        .GetCoinDataList(coinPosition)
                                        .GetTotalSize());
                    }
                }
                else
                {
                    if (coinPosition == Defines.CoinPosition.Field)
                    {
                        totalSize = duelData.FieldData
                                        .GetAllAreaCoins()
                                        .Sum(scc => scc.GetCoin().Size);
                    }
                    else
                    {
                        totalSize = duelData.Players.Sum(player => player
                                        .GetCoinDataList(coinPosition)
                                        .GetTotalSize());
                    }
                }

                if (totalSize < size) return false;
            }
            return true;
        }

        public override bool IsOnAreaEffect()
        {
            return false;
        }
    }
}
