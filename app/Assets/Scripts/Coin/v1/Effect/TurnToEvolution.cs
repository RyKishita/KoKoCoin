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
    class TurnToEvolution : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public TurnToEvolution(Defines.CoinPosition coinPosition, int turn, string coinName, bool bGreater)
        {
            if (coinPosition == Defines.CoinPosition.Field) throw new ArgumentException("TurnToEvolutionInField を使う");
            this.coinPosition = coinPosition;
            this.turn = turn;
            this.copiedCoinName = coinName;
            IsGreater = bGreater;
        }

        public override string Explain
        {
            get
            {
                int formatType;
                switch (coinPosition)
                {
                    case Defines.CoinPosition.Stock:formatType = 0; break;
                    case Defines.CoinPosition.Hand: formatType = 1; break;
                    default:throw new NotImplementedException();
                }

                string coinname = CoinList.Instance.GetCoin(copiedCoinName).DisplayName;
                string coinposition = Defines.GetLocalizedString(coinPosition);
                string stockcoin = Defines.GetLocalizedString(Defines.CoinPosition.Stock);

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinposition), coinposition },
                    { nameof(turn), turn.ToString()},
                    { nameof(coinname), coinname },
                    { nameof(stockcoin), stockcoin },
                };

                return GetEffectLocalizedString(nameof(TurnToEvolution), formatType, param);
            }
        }

        public bool IsGreater { get; }

        readonly Defines.CoinPosition coinPosition;
        readonly int turn;
        readonly string copiedCoinName;

        public override IEnumerable<string> GetCopiedCoinNames()
        {
            return base.GetCopiedCoinNames().Concat(new[] { copiedCoinName });
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, coinPosition)) return false;

            return duelEvent is AfterAddTurn afterTurn &&
                    afterTurn.TargetCoinDataIDs.Contains(selectedCoinData.CoinData.ID) &&
                    turn <= selectedCoinData.CoinData.Turn;
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            var duelData = duelManager.DuelData;
            if (IsReceiveEvent(duelData, selectedCoinData, after))
            {
                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo= selectedCoinData.CoinData.OwnerPlayerNo,
                    CoinPosition = coinPosition,
                    ParticleType = Defines.ParticleType.CreateCopy,
                    IsWaitPlayParticle = coinPosition == Defines.CoinPosition.Hand
                });

                duelManager.RegistDuelEventAction(new ActionMoveCoinsToExclusion()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    CoinMoveReason = Defines.CoinMoveReason.Effect
                });

                var copyCoin = duelManager.DuelData.CreateCopy(selectedCoinData.CoinData.ID, copiedCoinName, selectedCoinData.CoinData.OwnerPlayerNo);
                switch (coinPosition)
                {
                    case Defines.CoinPosition.Hand:
                    case Defines.CoinPosition.Stock:
                    case Defines.CoinPosition.Trash:
                        duelManager.RegistDuelEventAction(new ActionAddCopyToPlayer() { Target = copyCoin, CoinPosition = coinPosition });
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            return UniTask.CompletedTask;
        }

        public override bool? IsAdvantageProgressedTurn(DuelData duelData)
        {
            return IsGreater;
        }
    }
}
