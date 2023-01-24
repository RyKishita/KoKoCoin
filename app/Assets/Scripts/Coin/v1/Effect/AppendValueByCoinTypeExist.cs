using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByCoinTypeExist : AppendValueByCount
    {
        public AppendValueByCoinTypeExist(bool bCoinOwner, Defines.CoinPosition coinPosition, Defines.CoinType selectCoinType, int value)
            : base(1, value)
        {
            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
            this.selectCoinType = selectCoinType;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string coinpositionStr = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { "coinposition", coinpositionStr },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByCoinTypeExist), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition coinPosition;
        readonly Defines.CoinType selectCoinType;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            //var owner = selectedCoinData.CoinData.OwnerPlayerNo;
            //return coinPosition == Defines.CoinPosition.SettedCoins
            //            ? duelData.FieldData
            //                .GetAllAreaCoins()
            //                .Where(scc => bCoinOwner == (scc.CoinData.OwnerPlayerNo == owner))
            //                .Where(scc => scc.CoinData.ID != selectedCoinData.CoinData.ID)
            //                .Where(scc => scc.CoinBody.IsMatch(selectCoinType))
            //                .Count()
            //            : bCoinOwner
            //                ? duelData.GetCoins(owner, owner, coinPosition)
            //                    .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
            //                    .Where(cd => cd.Coin.Bodies.Any(body => body.IsMatch(selectCoinType)))
            //                    .Count()
            //                : duelData.GetOtherPlayersCoins(owner, owner, coinPosition)
            //                    .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
            //                    .Where(cd => cd.Coin.Bodies.Any(body => body.IsMatch(selectCoinType)))
            //                    .Count();
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
