using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsablePutCoins : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsablePutCoins(bool bCoinOwner, int num)
        {
            if (num <= 0) throw new ArgumentOutOfRangeException(nameof(num));
            this.bCoinOwner = bCoinOwner;
            this.num = num;
        }

        readonly bool bCoinOwner;
        readonly int num;

        public override string Explain
        {
            get
            {
                int formatType = bCoinOwner ? 0 : 1;
                string setcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);

                var param = new Dictionary<string, string>()
                {
                    { nameof(setcoin), setcoin},
                    { nameof(num), num.ToString()},
                };

                return GetEffectLocalizedString(nameof(CheckUsablePutCoins), formatType, param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                var playerNos = bCoinOwner
                                ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                                : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
                return playerNos.All(playerNo =>
                {
                    return duelData.FieldData.GetAreas().Max(area => area.GetCoinsByOwner(playerNo).Count) < num;
                });
            }

            return true;
        }

        public override bool IsProcessedOnArea()
        {
            return false;
        }
    }
}
