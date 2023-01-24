using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableLoopTurn : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableLoopTurn(int turn)
        {
            if (turn <= 0) throw new ArgumentOutOfRangeException(nameof(turn));
            this.turn = turn;
        }

        readonly int turn;

        public override string Explain
        {
            get
            {
                string handcoin = Defines.GetLocalizedString(Defines.CoinPosition.Hand);

                var param = new Dictionary<string, string>()
                {
                    { nameof(handcoin), handcoin },
                    { "value", turn.ToString() },
                };

                return GetEffectLocalizedString(nameof(CheckUsableLoopTurn), param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                int currentturn = selectedCoinData.CoinData.Turn;
                if (0 < currentturn && (currentturn % turn) == 0) return false;
            }

            return true;
        }

        public override bool IsProcessedOnArea()
        {
            return false;
        }
    }
}
