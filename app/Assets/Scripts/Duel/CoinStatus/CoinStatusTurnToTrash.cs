using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using MemoryPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Duel.CoinStatus
{
    [MemoryPackable]
    public partial class CoinStatusTurnToTrash : ICoinStatus, ICloneable
    {
        [MemoryPackConstructor]
        public CoinStatusTurnToTrash()
        {

        }

        public CoinStatusTurnToTrash(CoinStatusTurnToTrash src)
        {
            RegisteredPlayerNo = src.RegisteredPlayerNo;
            Turn = src.Turn;
        }

        #region serialize

        public int RegisteredPlayerNo { get; set; }

        /// <summary>
        /// 値
        /// </summary>
        public int Turn;

        #endregion

        public string GetName() => MakeName(Turn.ToString());

        public string GetExplain() => MakeExplain(Turn.ToString());

        public string GetNameExample() => MakeName("?");

        public string GetExplainExample() => MakeExplain("?");

        public object Clone()
        {
            return Duplicate();
        }

        public ICoinStatus Duplicate()
        {
            return new CoinStatusTurnToTrash(this);
        }

        public bool IsMatch(ICoinStatus other)
        {
            return other is CoinStatusTurnToTrash;
        }

        public void Marge(ICoinStatus other)
        {
            RegisteredPlayerNo = other.RegisteredPlayerNo;
            Turn = Math.Max(Turn, (other as CoinStatusTurnToTrash).Turn);
        }

        public UniTask ReceiveEvent(DuelManager duelManager, CoinData coinData, After duelEvent)
        {
            if (duelEvent is AfterStep afterStep && afterStep.GamePhase == Defines.DuelPhase.ターン終了)
            {
                var status = coinData.StatusList.GetItem<CoinStatusTurnToTrash>();
                if (status != null)
                {
                    status.Turn--;
                    if (status.Turn <= 0)
                    {
                        if (coinData.StatusList.RemoveBy(status))
                        {
                            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
                            {
                                CoinIDs = new List<int>() { coinData.ID },
                                CoinMoveReason = Defines.CoinMoveReason.CoinStatus,
                                DstCoinPosition = Defines.CoinPosition.Trash,
                                IsForce = true
                            });
                        }
                    }
                }
            }

            return UniTask.CompletedTask;
        }

        string MakeName(string tvalue)
        {
            var trashcoin = Defines.GetLocalizedString(Defines.CoinPosition.Trash);

            var param = new Dictionary<string, string>()
                    {
                        { nameof(tvalue), tvalue },
                        { nameof(trashcoin), trashcoin },
                    };
            return Functions.GetLocalizedStringName(nameof(CoinStatusTurnToTrash), param);
        }

        string MakeExplain(string tvalue)
        {
            var trashcoin = Defines.GetLocalizedString(Defines.CoinPosition.Trash);

            var param = new Dictionary<string, string>()
                    {
                        { nameof(tvalue), tvalue },
                        { nameof(trashcoin), trashcoin },
                    };
            return Functions.GetLocalizedString(nameof(CoinStatusTurnToTrash), param);
        }
    }
}
