using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class DontStop : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectMovable
    {
        public DontStop()
        {

        }

        public override string Explain => GetEffectLocalizedString(nameof(DontStop));

        public bool IsMovableArea(DuelData duelData, SelectedCoinData selectedCoinData, int areaNo)
        {
            var thisCoinAreaNo = duelData.FieldData.GetContainCoinAreaNo(selectedCoinData);
            return !thisCoinAreaNo.HasValue || thisCoinAreaNo.Value != areaNo;
        }
    }
}
