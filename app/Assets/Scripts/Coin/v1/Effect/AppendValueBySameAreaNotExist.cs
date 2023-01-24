using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueBySameAreaNotExist : AppendValueByCount
    {
        public AppendValueBySameAreaNotExist(bool bCoinOwner, int value)
            : base(0, value)
        {
            this.bCoinOwner = bCoinOwner;
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
                string setcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);

                var param = new Dictionary<string, string>()
                {
                    { nameof(setcoin), setcoin },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueBySameAreaNotExist), formatType, param);
            }
        }

        readonly bool bCoinOwner;

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
                        .Where(scc => scc.CoinData.ID != selectedCoinData.CoinData.ID)
                        .Count());
        }
    }
}
