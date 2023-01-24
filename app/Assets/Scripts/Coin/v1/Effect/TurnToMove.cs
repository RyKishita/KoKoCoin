using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class TurnToMove : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public TurnToMove(Defines.CoinPosition coinPosition, int turn, Defines.CoinPosition dstCoinPosition)
        {
            if (coinPosition == dstCoinPosition) throw new ArgumentException("移動元と移動先が同じ");

            this.SrcCoinPosition = coinPosition;
            this.turn = turn;
            this.DstCoinPosition = dstCoinPosition;
        }

        public Defines.CoinPosition SrcCoinPosition { get; }
        readonly int turn;
        public Defines.CoinPosition DstCoinPosition { get; }

        public override string Explain
        {
            get
            {
                int format;
                switch (SrcCoinPosition)
                {
                    case Defines.CoinPosition.Stock: format = 0; break;
                    case Defines.CoinPosition.Hand: format = 1; break;
                    case Defines.CoinPosition.Field: format = 2; break;
                    case Defines.CoinPosition.Trash: format = 3; break;
                    default: throw new NotImplementedException();
                }
                string dstcoinposition = Defines.GetLocalizedString(DstCoinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(turn), turn.ToString()},
                    { nameof(dstcoinposition), dstcoinposition},
                };

                return GetEffectLocalizedString(nameof(TurnToMove), format, param);
            }
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, SrcCoinPosition)) return false;

            return after is AfterAddTurn afterAddTurn &&
                afterAddTurn.TargetCoinDataIDs.Contains(selectedCoinData.CoinData.ID) &&
                turn <= selectedCoinData.CoinData.Turn;
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    DstCoinPosition = DstCoinPosition,
                    CoinMoveReason = Defines.CoinMoveReason.Effect
                });
            }

            return UniTask.CompletedTask;
        }

        public override bool? IsAdvantageProgressedTurn(DuelData duelData)
        {
            switch (SrcCoinPosition)
            {
                case Defines.CoinPosition.Stock:
                    switch (DstCoinPosition)
                    {
                        //case Defines.CoinPosition.山コイン:
                        case Defines.CoinPosition.Hand: return true;
                        case Defines.CoinPosition.Field: return true;
                        case Defines.CoinPosition.Trash:
                            // 山コインがカラになると敗北なら不利な効果。そうではない場合はデッキやルール次第だが不明としておく
                            return duelData.GameRule.DuelRule.GetEmptyDeckAction() == Defines.EmptyDeckAction.Lose ? (bool?)false : null;
                    }
                    break;
                case Defines.CoinPosition.Hand:
                    switch (DstCoinPosition)
                    {
                        case Defines.CoinPosition.Stock: return false;
                        //case Defines.CoinPosition.手持ちコイン:
                        case Defines.CoinPosition.Field: return true;
                        case Defines.CoinPosition.Trash: return false;
                    }
                    break;
                case Defines.CoinPosition.Field:
                    switch (DstCoinPosition)
                    {
                        case Defines.CoinPosition.Stock: return false;
                        case Defines.CoinPosition.Hand: return false;
                        //case Defines.CoinPosition.設置コイン:
                        case Defines.CoinPosition.Trash: return false;
                    }
                    break;
                case Defines.CoinPosition.Trash:
                    switch (DstCoinPosition)
                    {
                        case Defines.CoinPosition.Stock: return true;
                        case Defines.CoinPosition.Hand: return true;
                        case Defines.CoinPosition.Field: return true;
                            //case Defines.CoinPosition.捨てコイン:
                    }
                    break;
            }
            return null;
        }

        public override bool IsAdvantage(DuelData duelData)
        {
            switch (SrcCoinPosition)
            {
                case Defines.CoinPosition.Stock:
                    switch (DstCoinPosition)
                    {
                        //case Defines.CoinPosition.山コイン:
                        case Defines.CoinPosition.Hand: return true;
                        case Defines.CoinPosition.Field: return true;
                        case Defines.CoinPosition.Trash:
                            // 山コインがカラになった敗北なら不利な効果。そうではない場合はデッキやルール次第だが、有利な効果としておく
                            return duelData.GameRule.DuelRule.GetEmptyDeckAction() == Defines.EmptyDeckAction.Lose ? false : true;
                    }
                    break;
                case Defines.CoinPosition.Hand:
                    switch (DstCoinPosition)
                    {
                        case Defines.CoinPosition.Stock: return false;
                        //case Defines.CoinPosition.手持ちコイン:
                        case Defines.CoinPosition.Field: return true;
                        case Defines.CoinPosition.Trash: return false;
                    }
                    break;
                case Defines.CoinPosition.Field:
                    switch (DstCoinPosition)
                    {
                        case Defines.CoinPosition.Stock: return false;
                        case Defines.CoinPosition.Hand: return false;
                        //case Defines.CoinPosition.設置コイン:
                        case Defines.CoinPosition.Trash: return false;
                    }
                    break;
                case Defines.CoinPosition.Trash:
                    switch (DstCoinPosition)
                    {
                        case Defines.CoinPosition.Stock: return true;
                        case Defines.CoinPosition.Hand: return true;
                        case Defines.CoinPosition.Field: return true;
                            //case Defines.CoinPosition.捨てコイン:
                    }
                    break;
            }
            return false;
        }
    }
}
