using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class TurnToEvolutionInField : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public TurnToEvolutionInField(int turn, string coinName, bool bGreater)
        {
            this.turn = turn;
            this.copiedCoinName = coinName;
            IsGreater = bGreater;
        }

        public override string Explain
        {
            get
            {
                string coinname = CoinList.Instance.GetCoin(copiedCoinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(turn), turn.ToString()},
                    { nameof(coinname), coinname },
                };

                return GetEffectLocalizedString(nameof(TurnToEvolutionInField), param);
            }
        }

        public bool IsGreater { get; }

        readonly int turn;
        readonly string copiedCoinName;

        public override IEnumerable<string> GetCopiedCoinNames()
        {
            return base.GetCopiedCoinNames().Concat(new[] { copiedCoinName });
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            return duelEvent is AfterAddTurn afterTurn &&
                    afterTurn.TargetCoinDataIDs.Contains(selectedCoinData.CoinData.ID) &&
                    turn <= selectedCoinData.CoinData.Turn;
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            var duelData = duelManager.DuelData;
            if (IsReceiveEvent(duelData, selectedCoinData, after))
            {
                duelManager.RegistDuelEventAction(new ActionEffectSetCoin()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    ParticleType = Defines.ParticleType.CreateCopy
                });

                duelManager.RegistDuelEventAction(new ActionMoveCoinsToExclusion()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    CoinMoveReason = Defines.CoinMoveReason.Effect
                });

                var copyCoin = duelManager.DuelData.CreateCopy(selectedCoinData.CoinData.ID, copiedCoinName, selectedCoinData.CoinData.OwnerPlayerNo);
                var areaNo = duelData.FieldData.GetContainCoinAreaNo(selectedCoinData);
                duelManager.RegistDuelEventAction(new ActionAddCopyToSet()
                {
                    Target = copyCoin.MakeSetCoin(),
                    AreaNo = areaNo.Value
                });
            }
            return UniTask.CompletedTask;
        }

        public override bool? IsAdvantageProgressedTurn(DuelData duelData)
        {
            return IsGreater;
        }
    }
}
