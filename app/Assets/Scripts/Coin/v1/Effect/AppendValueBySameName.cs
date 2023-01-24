using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AppendValueBySameName : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectAppendValue
    {
        public AppendValueBySameName()
        {

        }

        public override string Explain
        {
            get
            {
                string handcoin = Defines.GetLocalizedString(Defines.CoinPosition.Hand);

                var param = new Dictionary<string, string>()
                {
                    { nameof(handcoin), handcoin },
                };

                return GetEffectLocalizedString(nameof(AppendValueBySameName), param);
            }
        }

        public int GetAppendValue(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            int count = duelData.Players[playerNo]
                    .Hand
                    .Items
                    .Where(cd => cd.ID != selectedCoinData.CoinData.ID)
                    .Where(cd => cd.CoinName == selectedCoinData.CoinData.CoinName)
                    .Count();
            return count * baseValue;
        }
    }
}
