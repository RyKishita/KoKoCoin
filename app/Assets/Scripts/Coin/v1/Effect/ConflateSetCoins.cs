using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class ConflateSetCoins : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public ConflateSetCoins(int num, string copiedCoinName, int selectedBodyIndex)
        {
            if (num <= 0) throw new ArgumentOutOfRangeException(nameof(num));
            this.num = num;
            this.copiedCoinName = copiedCoinName;
            this.selectedBodyIndex = selectedBodyIndex;
        }

        public override string Explain
        {
            get
            {
                string trashcoin = Defines.GetLocalizedString(Defines.CoinPosition.Trash);
                string coinname = CoinList.Instance.GetCoin(copiedCoinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(trashcoin), trashcoin },
                    { nameof(coinname), coinname },
                    { nameof(num), num.ToString() }
                };

                return GetEffectLocalizedString(nameof(ConflateSetCoins), param);
            }
        }

        readonly int num;
        readonly string copiedCoinName;
        readonly int selectedBodyIndex;

        IEnumerable<int> GetAreaCoins(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field))
            {
                if (duelEvent is AfterMoveCoinsToSet duelEventAfterMoveCoinsToSet)
                {
                    int areaNo = duelEventAfterMoveCoinsToSet.DstAreaNo;
                    var fieldData = duelData.FieldData;
                    if (fieldData.GetContainCoinAreaNo(selectedCoinData) == areaNo)
                    {
                        var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                        return fieldData.AreaDatas[areaNo].GetCoinsByOwner(playerNo).Select(scc=>scc.CoinData.ID);
                    }
                }
            }
            return Enumerable.Empty<int>();
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return num <= GetAreaCoins(duelData, selectedCoinData, duelEvent).Count();
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After duelEvent)
        {
            var coinIDs = GetAreaCoins(duelManager.DuelData, selectedCoinData, duelEvent).ToList();
            if (num <= coinIDs.Count())
            {
                var duelEventAfterMoveCoinsToSet = duelEvent as AfterMoveCoinsToSet;

                duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
                {
                    CoinIDs = coinIDs,
                    DstCoinPosition = Defines.CoinPosition.Trash,
                    CoinMoveReason = Defines.CoinMoveReason.Effect
                });

                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                var token = duelManager.DuelData.CreateCopy(selectedCoinData.CoinData.ID, copiedCoinName, playerNo, selectedBodyIndex);

                duelManager.RegistDuelEventAction(new ActionAddCopyToSet()
                {
                    Target = token,
                    AreaNo = duelEventAfterMoveCoinsToSet.DstAreaNo
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
