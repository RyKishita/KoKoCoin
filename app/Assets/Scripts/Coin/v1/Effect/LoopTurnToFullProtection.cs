using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class LoopTurnToFullProtection : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectGuardFull
    {
        public LoopTurnToFullProtection(int turn)
        {
            if (turn < 2) throw new ArgumentOutOfRangeException(nameof(turn));
            this.turn = turn;
        }

        readonly int turn;

        public override string Explain
        {
            get
            {
                string coinposition = Defines.GetLocalizedString(Defines.CoinPosition.Hand);

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinposition), coinposition },
                    { "value", turn.ToString() },
                };

                return GetEffectLocalizedString(nameof(LoopTurnToFullProtection), param);
            }
        }

        
        public bool IsFullGuard(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData directAttackSelectedCoinData)
        {
            int currentTurn = selectedCoinData.CoinData.Turn;
            return (0 < currentTurn && (currentTurn % turn) == 0);
        }
    }
}
