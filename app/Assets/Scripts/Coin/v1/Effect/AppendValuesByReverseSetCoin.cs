using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValuesByReverseSetCoin : AppendValuesByCount
    {
        public AppendValuesByReverseSetCoin(bool bCoinOwner, int value)
            : base(value)
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
                string reverse = Defines.GetLocalizedString(Defines.StringEnum.HiddenSide);
                string setcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);

                var param = new Dictionary<string, string>()
                {
                    { nameof(reverse), reverse },
                    { nameof(setcoin), setcoin },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValuesByReverseSetCoin), formatType, param);
            }
        }

        readonly bool bCoinOwner;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNos = bCoinOwner
                            ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                            : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
            return duelData.FieldData.GetAllAreaCoins()
                            .Where(scd => playerNos.Contains(scd.CoinData.OwnerPlayerNo))
                            .Where(scd => scd.CoinData.ID != selectedCoinData.CoinData.ID)
                            .Where(scd => scd.IsReverse)
                            .Count();
        }
    }
}
