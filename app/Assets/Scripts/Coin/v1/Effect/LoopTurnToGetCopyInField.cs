using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class LoopTurnToGetCopyInField : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public LoopTurnToGetCopyInField(int loopTurn, string copiedCoinName)
        {
            if (loopTurn <= 0) throw new ArgumentOutOfRangeException(nameof(loopTurn));
            this.loopTurn = loopTurn;
            this.copiedCoinName = copiedCoinName;
        }

        public override string Explain
        {
            get
            {
                int formatType = (loopTurn == 1) ? 0 : 1;

                string coinname = CoinList.Instance.GetCoin(copiedCoinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { "loopturn", loopTurn.ToString() },
                    { nameof(coinname), coinname},
                };

                return GetEffectLocalizedString(nameof(LoopTurnToGetCopyInField), formatType, param);
            }
        }

        readonly int loopTurn;
        readonly string copiedCoinName;

        public override IEnumerable<string> GetCopiedCoinNames()
        {
            return base.GetCopiedCoinNames().Concat(new[] { copiedCoinName });
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field))
            {
                if (after is AfterAddTurn duelEventAfterTurn &&
                    duelEventAfterTurn.TargetCoinDataIDs.Contains(selectedCoinData.CoinData.ID))
                {
                    int currentTurn = selectedCoinData.CoinData.Turn;
                    if (0 < currentTurn && (currentTurn % loopTurn) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                var player = duelManager.DuelData.Players[playerNo];

                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = playerNo,
                    ParticleType = Defines.ParticleType.CreateCopy,
                    IsWaitPlayParticle = true
                });

                var token = duelManager.DuelData.CreateCopy(selectedCoinData.CoinData.ID, copiedCoinName, player);
                duelManager.RegistDuelEventAction(new ActionAddCopyToPlayer()
                {
                    Target = token,
                    CoinPosition = Defines.CoinPosition.Hand
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
