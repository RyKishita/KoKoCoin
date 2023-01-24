using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class SetAllAreaTypeToWin : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public SetAllAreaTypeToWin()
        {

        }

        public override string Explain => GetEffectLocalizedString(nameof(SetAllAreaTypeToWin));

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            if (duelEvent is AfterMoveCoinsToSet || duelEvent is AfterModifyArea)
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                var areaTypes = duelData.FieldData.GetAreas()
                                    .Where(areaData => areaData
                                        .GetCoinsByOwner(playerNo)
                                        .Where(scd => scd.CoinData.CoinName == selectedCoinData.CoinData.CoinName)
                                        .Any())
                                    .Select(areaData => areaData.GetAreaType())
                                    .Distinct()
                                    .ToList();
                if (areaTypes.Contains(Defines.AreaType.A) &&
                    areaTypes.Contains(Defines.AreaType.B) &&
                    areaTypes.Contains(Defines.AreaType.C) &&
                    areaTypes.Contains(Defines.AreaType.D))
                {
                    return true;
                }
            }

            return false;
        }

        public async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

                var coinName = selectedCoinData.CoinData.CoinName;

                var coinIDs = duelManager.DuelData.FieldData
                                .GetAreas()
                                .SelectMany(areaData => areaData.GetCoinsByOwner(playerNo)
                                    .Where(scd => scd.CoinData.CoinName == coinName)
                                    .Select(scd => scd.CoinData.ID));

                var targetObjects = duelManager.GetObjectsByCoin(coinIDs).ToList();

                await duelManager.ProcessExtraWinAsync(playerNo, Main.RevolutionByCrowd.name, targetObjects);
            }
        }
    }
}
