using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableNeedExtraCostBySize : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable, Scripts.Coin.Effect.IEffectNeedExtraCost
    {
        public CheckUsableNeedExtraCostBySize(int totalSize)
        {
            Value = totalSize;
        }

        public int Value { get; }

        public override string Explain
        {
            get
            {
                string handcoin = Defines.GetLocalizedString(Defines.CoinPosition.Hand);
                string trashcoin = Defines.GetLocalizedString(Defines.CoinPosition.Trash);

                var param = new Dictionary<string, string>()
                {
                    { nameof(handcoin), handcoin },
                    { "totalsize", Value.ToString() },
                    { nameof(trashcoin), trashcoin},
                };

                return GetEffectLocalizedString(nameof(CheckUsableNeedExtraCostBySize), param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                var coinDataList = duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].Hand.Duplicate();
                coinDataList.UnRegist(selectedCoinData.CoinData);
                if (coinDataList.GetTotalSize() < Value) return false;
            }
            return true;
        }

        public bool IsValidExtraCost(List<CoinData> coinDatas)
        {
            return Value <= coinDatas.Sum(cc => cc.GetCoin().Size);
        }

        public override bool IsProcessedOnArea()
        {
            return false;
        }
    }
}
