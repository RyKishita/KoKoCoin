using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AddValueStatusByLoopTurn : AddValueStatusBy
    {
        public AddValueStatusByLoopTurn(int value, int turn)
            : base(value)
        {
            this.turn = turn;
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
                    { nameof(turn), turn.ToString()},
                    { "value", Math.Abs(Value).ToString() },
                };

                return GetEffectLocalizedString(nameof(AddValueStatusByLoopTurn), formatType, param);
            }
        }

        readonly int turn;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            if (duelEvent is AfterAddTurn afterTurn &&
                afterTurn.TargetCoinDataIDs.Contains(selectedCoinData.CoinData.ID))
            {
                var coinTurn = selectedCoinData.CoinData.Turn;
                if (0 < coinTurn && (coinTurn % turn) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
