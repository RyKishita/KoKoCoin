using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueByCoinNumCompareEqual : AppendValueByCount
    {
        public AppendValueByCoinNumCompareEqual(Defines.CoinPosition coinPosition, int value)
            : base(0, value)
        {
            this.coinPosition = coinPosition;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string player1 = Defines.GetLocalizedString(Defines.StringEnum.You);
                string player2 = Defines.GetLocalizedString(Defines.StringEnum.Opponent);
                string coinpositionStr = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player1), player1 },
                    { nameof(player2), player2 },
                    { "coinposition", coinpositionStr },
                    { nameof(num), Math.Abs(num).ToString() },
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AppendValueByCoinNumCompareEqual), formatType, param);
            }
        }

        readonly Defines.CoinPosition coinPosition;

        protected override int GetCount(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            var otherPlayerNos = duelData.GetOtherTeamPlayerNos(playerNo).ToList();

            int numOwner;
            int numOther;
            if (coinPosition == Defines.CoinPosition.Field)
            {
                numOwner = duelData.FieldData.GetAllAreaCoins().Where(scd => scd.CoinData.OwnerPlayerNo == playerNo).Count();
                numOther = duelData.FieldData.GetAllAreaCoins().Where(scd => otherPlayerNos.Contains(scd.CoinData.OwnerPlayerNo)).Count();
            }
            else
            {
                numOwner = duelData.Players[playerNo].GetCoinDataList(coinPosition).GetCount();
                numOther = otherPlayerNos.Sum(otherPlayerNo => duelData.Players[otherPlayerNo].GetCoinDataList(coinPosition).GetCount());
            }

            return numOwner - numOther;
        }
    }
}
