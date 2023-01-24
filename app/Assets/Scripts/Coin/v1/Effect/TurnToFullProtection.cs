using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class TurnToFullProtection : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectGuardFull
    {
        public TurnToFullProtection(int turn)
        {
            this.turn = turn;
        }

        readonly int turn;

        public override string Explain
        {
            get
            {
                var param = new Dictionary<string, string>()
                {
                    { nameof(turn), turn.ToString()},
                };

                return GetEffectLocalizedString(nameof(TurnToFullProtection), param);
            }
        }

        public bool IsFullGuard(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData directAttackSelectedCoinData)
        {
            return turn <= selectedCoinData.CoinData.Turn;
        }

        public override bool? IsAdvantageProgressedTurn(Duel.DuelData duelData)
        {
            return true;
        }
    }
}
