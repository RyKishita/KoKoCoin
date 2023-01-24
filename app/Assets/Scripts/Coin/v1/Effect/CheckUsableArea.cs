using Assets.Scripts.Duel;
using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableArea : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableArea(params Defines.AreaType[] areaTypes)
        {
            this.areaTypes = areaTypes;
        }

        readonly Defines.AreaType[] areaTypes;

        public override string Explain
        {
            get
            {
                string areatype = ZString.Join(",", areaTypes);

                var param = new Dictionary<string, string>()
                {
                    { nameof(areatype), areatype },
                };

                return GetEffectLocalizedString(nameof(CheckUsableArea), param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                var player = duelData.Players[playerNo];
                var area = duelData.FieldData.AreaDatas[player.CurrentAreaNo];
                if (!areaTypes.Contains(area.GetAreaType())) return false;
            }
            return true;
        }

        public override bool IsProcessedOnArea()
        {
            return false;
        }

        public override bool IsAdvantage(DuelData duelData)
        {
            return false;
        }
    }
}
