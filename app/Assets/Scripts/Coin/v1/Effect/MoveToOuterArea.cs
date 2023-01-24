using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class MoveToOuterArea : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public MoveToOuterArea()
        {

        }

        public override string Explain
        {
            get
            {
                string trashcoin = Defines.GetLocalizedString(Defines.CoinPosition.Trash);

                var param = new Dictionary<string, string>()
                {
                    { nameof(trashcoin), trashcoin},
                };

                return GetEffectLocalizedString(nameof(MoveToOuterArea), param);
            }
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field) &&
                duelEvent is AfterStep afterStep &&
                afterStep.GamePhase == Defines.DuelPhase.ターン終了;
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                var areaNo = duelManager.DuelData.FieldData.GetContainCoinAreaNo(selectedCoinData);
                if (areaNo.HasValue)
                {
                    int newAreaNo = areaNo.Value;

                    int areaCenterNo = duelManager.DuelData.FieldData.AreaCenterNo;
                    if (newAreaNo == areaCenterNo)
                    {
                        if (duelManager.DuelData.GameRule.DuelRule.GetPlayerStartAreaNo()[selectedCoinData.CoinData.OwnerPlayerNo] < areaCenterNo)
                        {
                            newAreaNo--;
                        }
                        else
                        { 
                            newAreaNo++;
                        }
                    }
                    else
                    {
                        if (newAreaNo < areaCenterNo)
                        {
                            newAreaNo--;
                        }
                        else
                        {
                            newAreaNo++;
                        }
                    }

                    var areadatas = duelManager.DuelData.FieldData.AreaDatas;
                    if (!areadatas.ContainsKey(newAreaNo) || areadatas[newAreaNo].GetAreaType() == Defines.AreaType.Safe)
                    {
                        duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
                        {
                            CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                            CoinMoveReason = Defines.CoinMoveReason.Effect,
                            DstCoinPosition = Defines.CoinPosition.Trash,
                        });
                    }
                    else
                    {
                        duelManager.RegistDuelEventAction(new ActionMoveCoinsToSet()
                        {
                            CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                            CoinMoveReason = Defines.CoinMoveReason.Effect,
                            DstAreaNo = newAreaNo,
                        });
                    }
                }
            }

            return UniTask.CompletedTask;
        }
    }
}
