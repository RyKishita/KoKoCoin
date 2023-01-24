using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByReverseSetCoinOver : AppendValueByCount
    {
        public AppendValueByReverseSetCoinOver(bool bCoinOwner, int num, int value)
            : base(num, value)
        {
            if (num <= 0)throw new ArgumentOutOfRangeException(nameof(num));
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
                    { nameof(num), num.ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByReverseSetCoinOver), formatType, param);
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
