using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class MoveToInnerArea : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public MoveToInnerArea()
        {

        }

        public override string Explain => GetEffectLocalizedString(nameof(MoveToInnerArea));

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field) &&
                    after is AfterStep afterStep &&
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
                    if (newAreaNo != areaCenterNo)
                    {
                        if (newAreaNo < areaCenterNo)
                        {
                            newAreaNo++;
                        }
                        else
                        {
                            newAreaNo--;
                        }

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
