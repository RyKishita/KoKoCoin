using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueBySameAreaTagExist : AppendValueByCount
    {
        public AppendValueBySameAreaTagExist(bool bCoinOwner, Defines.CoinTag coinTag, int value)
            : base(1, value)
        {
            this.bCoinOwner = bCoinOwner;
            this.coinTag = coinTag;
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
                string tagname = Defines.GetLocalizedString(coinTag);
                string setcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);

                var param = new Dictionary<string, string>()
                {
                    { nameof(tagname), tagname },
                    { nameof(setcoin), setcoin },
                    { nameof(num), num.ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueBySameAreaTagExist), formatType, param);
            }
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinTag coinTag;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var areaNo = duelData.FieldData.GetContainCoinAreaNo(selectedCoinData);
            if (!areaNo.HasValue) return 0;

            var playerNos = bCoinOwner
                ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();

            var areaData = duelData.FieldData.AreaDatas[areaNo.Value];
            return playerNos.Max(playerNo => areaData
                        .GetCoinsByOwner(playerNo)
                        .Where(scd => scd.CoinData.ID != selectedCoinData.CoinData.ID)
                        .Where(scd => scd.GetCoinTag().HasFlag(coinTag))
                        .Count());
        }
    }
}
