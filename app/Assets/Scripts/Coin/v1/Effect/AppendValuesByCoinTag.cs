using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValuesByCoinTag : AppendValuesByCount
    {
        public AppendValuesByCoinTag(bool bCoinOwner, Defines.CoinPosition coinPosition, Defines.CoinTag coinTag, int value)
            : base(value)
        {
            if (coinPosition == Defines.CoinPosition.Field) throw new ArgumentException("AppendValuesByCoinTagInField を使う");
            if (!bCoinOwner && (coinPosition == Defines.CoinPosition.Stock || coinPosition == Defines.CoinPosition.Hand)) throw new ArgumentException("相手のコインを探れる指定はできない");
            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
            this.coinTag = coinTag;
        }

        public override string Explain
        {
            get
            {
                int format = (0 < Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    format += 2;
                }
                string tagname = Defines.GetLocalizedString(coinTag);
                string coinposition = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(tagname), tagname },
                    { nameof(coinposition), coinposition },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValuesByCoinTag), format, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition coinPosition;
        readonly Defines.CoinTag coinTag;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
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
    }
}
