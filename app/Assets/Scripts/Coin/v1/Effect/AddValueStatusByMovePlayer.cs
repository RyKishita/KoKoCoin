using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByMovePlayer : AddValueStatusBy
    {
        public AddValueStatusByMovePlayer(int value, Defines.AreaType areaType)
            : base(value)
        {
            this.areaType = areaType;
        }

        public override string Explain
        {
            get
            {
                int formatType = (0 < Value) ? 0 : 1;
                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);

                var param = new Dictionary<string, string>()
                {
                    { nameof(field), field },
                    { nameof(areaType), areaType.ToString()},
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByMovePlayer), formatType, param);
            }
        }

        readonly Defines.AreaType areaType;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            var fieldData = duelData.FieldData;
            return duelEvent is AfterMovePlayer afterMovePlayer&&
                    fieldData.AreaDatas[afterMovePlayer.AreaNo].GetAreaType() == areaType;
        }
    }
}
