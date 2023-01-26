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
    public partial class CoinStatusInvalidEffectByTurn : ICoinStatus, ICloneable
    {
        [MemoryPackConstructor]
        public CoinStatusInvalidEffectByTurn()
        {

        }

        public CoinStatusInvalidEffectByTurn(CoinStatusInvalidEffectByTurn src)
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
            return new CoinStatusInvalidEffectByTurn(this);
        }

        public bool IsMatch(ICoinStatus other)
        {
            return other is CoinStatusInvalidEffectByTurn;
        }

        public void Marge(ICoinStatus other)
        {
            RegisteredPlayerNo = other.RegisteredPlayerNo;
            Turn = Math.Max(Turn, (other as CoinStatusInvalidEffectByTurn).Turn);
        }

        public async UniTask ReceiveEvent(DuelManager duelManager, CoinData coinData, After duelEvent)
        {
            if (duelEvent is AfterStep afterStep && afterStep.GamePhase == Defines.DuelPhase.ターン終了)
            {
                var status = coinData.StatusList.GetItem<CoinStatusInvalidEffectByTurn>();
                if (status != null)
                {
                    status.Turn--;
                    if (status.Turn <= 0)
                    {
                        if (coinData.StatusList.RemoveBy(status))
                        {
                            var particleType = Defines.ParticleType.Curse;

                            var coinPosition = duelManager.DuelData.GetCoinPosition(coinData);
                            if (coinPosition == Defines.CoinPosition.Field)
                            {
                                duelManager.RegistDuelEventAction(new ActionEffectSetCoin()
                                {
                                    CoinIDs = new List<int>() { coinData.ID },
                                    ParticleType = particleType
                                });
                            }
                            else
                            {
                                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                                {
                                    PlayerNo = coinData.OwnerPlayerNo,
                                    CoinPosition = coinPosition,
                                    ParticleType = particleType
                                });
                            }

                            await duelManager.ReceiveEventAsync(new AfterCleanupCoinStatus()
                            {
                                CoinID = coinData.ID,
                                CoinStatus = status,
                            });
                        }
                    }
                }
            }
        }

        string MakeName(string tvalue)
        {
            var param = new Dictionary<string, string>()
            {
                { nameof(tvalue), tvalue },
            };
            return Functions.GetLocalizedStringName(nameof(CoinStatusInvalidEffectByTurn), param);
        }
        
        string MakeExplain(string tvalue)
        {
            var param = new Dictionary<string, string>()
            {
                { nameof(tvalue), tvalue },
            };
            return Functions.GetLocalizedString(nameof(CoinStatusInvalidEffectByTurn), param);
        }
    }
}
