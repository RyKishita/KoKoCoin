using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByCoinTagOver : AppendValueByCount
    {
        public AppendValueByCoinTagOver(bool bCoinOwner, Defines.CoinPosition coinPosition, Defines.CoinTag coinTag, int num, int value)
            : base(num, value)
        {
            if (num <= 0) throw new ArgumentOutOfRangeException(nameof(num));
            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
            this.coinTag = coinTag;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string tagname = Defines.GetLocalizedString(coinTag);
                string coinpositionStr = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(tagname), tagname },
                    { "coinposition", coinpositionStr },
                    { nameof(num), Math.Abs(num).ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByCoinTagOver), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition coinPosition;
        readonly Defines.CoinTag coinTag;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            //var owner = selectedCoinData.CoinData.OwnerPlayerNo;

            //return (coinPosition == Defines.CoinPosition.SettedCoins)
            //            ? duelData.FieldData
            //                .GetAllAreaCoins()
            //                .Where(scd => bCoinOwner == (scd.CoinData.OwnerPlayerNo == owner))
            //                .Where(scc => scc.CoinData.ID != selectedCoinData.CoinData.ID)
            //                .Where(scc => scc.CoinTag.HasFlag(coinTag))
            //                .Count()
            //            : bCoinOwner
            //                ? duelData.GetCoins(owner, owner, coinPosition)
            //                    .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
            //                    .Where(cd => cd.HasTag(coinTag))
            //                    .Count()
            //                : duelData.GetOtherPlayersCoins(owner, owner, coinPosition)
            //                    .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
            //                    .Where(cd => cd.HasTag(coinTag))
            //                    .Count();
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            if (coinPosition == Defines.CoinPosition.Field)
            {
                return duelData
                        .FieldData
                        .GetAllAreaCoins()
                        .Where(scd => scd.CoinData.ID != selectedCoinData.CoinData.ID)
                        .Where(scd => bCoinOwner == (scd.CoinData.OwnerPlayerNo == playerNo))
                        .Where(scd => scd.GetCoinTag().HasFlag(coinTag))
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
        }
    }
}
