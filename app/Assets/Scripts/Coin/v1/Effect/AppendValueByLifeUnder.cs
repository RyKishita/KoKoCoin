using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByLifeUnder : AppendValueByCount
    {
        public AppendValueByLifeUnder(bool bCoinOwner, int life, int value)
            : base(-life, value)
        {
            if (life <= 0) throw new ArgumentOutOfRangeException(nameof(life));

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
                var param = new Dictionary<string, string>()
                {
                    { nameof(num), Math.Abs(num).ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByLifeUnder), formatType, param);
            }
        }

        readonly bool bCoinOwner;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNos = bCoinOwner
                            ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                            : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
            return playerNos.Max(playerNo => duelData.Players[playerNo].Life);
        }
    }
}
