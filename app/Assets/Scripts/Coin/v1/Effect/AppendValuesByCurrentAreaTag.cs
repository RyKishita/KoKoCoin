using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValuesByCurrentAreaTag : AppendValuesByCount
    {
        public AppendValuesByCurrentAreaTag(bool bCoinOwner, Defines.CoinTag coinTag, int value)
            : base(value)
        {
            this.bCoinOwner = bCoinOwner;
            TargetCoinTag = coinTag;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                if (!bCoinOwner)
                {
                    formatType += 2;
                }
                string tagname = Defines.GetLocalizedString(TargetCoinTag);

                var param = new Dictionary<string, string>()
                {
                    { nameof(tagname), tagname },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValuesByCurrentAreaTag), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        public Defines.CoinTag TargetCoinTag { get; }

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            var currentAreaNo = duelData.Players[playerNo].CurrentAreaNo;
            return duelData.FieldData
                        .AreaDatas[currentAreaNo]
                        .Coins
                        .Where(scd => scd.CoinData.OwnerPlayerNo == playerNo)
                        .Where(scd => scd.CoinData.ID != selectedCoinData.CoinData.ID)
                        .Where(scd => scd.GetCoinTag().HasFlag(TargetCoinTag))
                        .Count();
        }
    }
}
