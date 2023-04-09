using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableCoinNumCompare : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableCoinNumCompare(Defines.CoinPosition coinPosition, int num)
        {
            this.coinPosition = coinPosition;
            this.num = num;
        }

        readonly Defines.CoinPosition coinPosition;
        readonly int num;

        public override string Explain
        {
            get
            {
                int formatType;
                if (0 < num)
                {
                    if (num == 1)
                    {
                        formatType = 0;
                    }
                    else
                    {
                        formatType = 1;
                    }
                }
                else if (num < 0)
                {
                    if (Math.Abs(num) == 1)
                    {
                        formatType = 1;
                    }
                    else
                    {
                        formatType = 2;
                    }
                }
                else
                {
                    formatType = 3;
                }

                string player1 = Defines.GetLocalizedString(Defines.StringEnum.You);
                string player2 = Defines.GetLocalizedString(Defines.StringEnum.Opponent);
                string coinposition = Defines.GetLocalizedString(coinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player1), player1 },
                    { nameof(player2), player2 },
                    { nameof(coinposition), coinposition },
                    { nameof(num), Math.Abs(num).ToString() },
                };

                return GetEffectLocalizedString(nameof(CheckUsableCoinName), formatType, param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                var otherPlayerNos = duelData.GetOtherTeamPlayerNos(playerNo);

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

                int count = numOwner - numOther;

                if (0 < num)
                {
                    if (count < num) return false;
                }
                else if (num < 0)
                {
                    if (Math.Abs(num) < count) return false;
                }
                else
                {
                    if (count != 0) return false;
                }
            }
            return true;
        }

        public override bool IsOnAreaEffect()
        {
            return false;
        }
    }
}
