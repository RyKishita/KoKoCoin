using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByReverseSetCoinNotExist : AppendValueByCount
    {
        public AppendValueByReverseSetCoinNotExist(bool bCoinOwner, int value)
            : base(0, value)
        {
            this.bCoinOwner = bCoinOwner;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string reverse = Defines.GetLocalizedString(Defines.StringEnum.HiddenSide);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(reverse), reverse },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByReverseSetCoinNotExist), formatType, param);
            }
        }

        readonly bool bCoinOwner;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNos = bCoinOwner
                ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();

            return duelData.FieldData
                            .GetAllAreaCoins()
                            .Where(scd => playerNos.Contains(scd.CoinData.OwnerPlayerNo))
                            .Where(scd => scd.CoinData.ID != selectedCoinData.CoinData.ID)
                            .Where(scd => scd.IsReverse)
                            .Count();
        }
    }
}
