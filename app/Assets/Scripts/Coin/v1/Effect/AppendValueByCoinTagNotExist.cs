using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByCoinTagNotExist : AppendValueByCount
    {
        public AppendValueByCoinTagNotExist(bool bCoinOwner, Defines.CoinPosition coinPosition, Defines.CoinTag coinTag, int value)
            : base(0, value)
        {
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

                return GetEffectLocalizedString(nameof(AppendValueByCoinTagNotExist), formatType,param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition coinPosition;
        readonly Defines.CoinTag coinTag;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
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
