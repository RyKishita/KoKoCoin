using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AreaCoinNumToWin : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public AreaCoinNumToWin(int num)
        {
            if(num <= 0) throw new ArgumentOutOfRangeException(nameof(num));
            this.num = num;
        }

        public override string Explain
        {
            get
            {
                var param = new Dictionary<string, string>()
                {
                    { nameof(num), num.ToString() },
                };

                return GetEffectLocalizedString(nameof(AreaCoinNumToWin), param);
            }
        }

        readonly int num;

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            Func<int, bool> isMatch = areaNo => num <= duelData.FieldData.AreaDatas[areaNo].GetCoinsByOwner(selectedCoinData.CoinData.OwnerPlayerNo).Count();

            return after is AfterMoveCoinsToSet duelEventAfterMoveCoinsToSet &&
                    isMatch(duelEventAfterMoveCoinsToSet.DstAreaNo) ||
                    after is AfterCleanupCoinStatus afterCleanupCoinStatus &&
                    afterCleanupCoinStatus.CoinID == selectedCoinData.CoinData.ID &&
                    afterCleanupCoinStatus.CoinStatus is Duel.CoinStatus.CoinStatusInvalidEffectByTurn &&
                    duelData.GetCoinPosition(selectedCoinData) == Defines.CoinPosition.Field &&
                    isMatch(duelData.FieldData.GetContainCoinAreaNo(selectedCoinData).Value);
        }

        public async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                var duelEventAfterMoveCoinsToSet = after as AfterMoveCoinsToSet;

                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

                var coinIDs = duelManager.DuelData.FieldData
                                .AreaDatas[duelEventAfterMoveCoinsToSet.DstAreaNo]
                                .GetCoinsByOwner(playerNo)
                                .Select(scd => scd.CoinData.ID);

                var targetObjects = duelManager.GetObjectsByCoin(coinIDs).ToList();

                await duelManager.ProcessExtraWinAsync(playerNo, Main.WinFlag.name, targetObjects);
            }
        }
    }
}
