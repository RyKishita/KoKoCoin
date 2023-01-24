using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class AfterStopSideCopy : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public AfterStopSideCopy(string copiedCoinName)
        {
            this.copiedCoinName = copiedCoinName;
        }

        public override string Explain
        {
            get
            {
                string coinname = CoinList.Instance.GetCoin(copiedCoinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinname), coinname },
                };

                return GetEffectLocalizedString(nameof(AfterStopSideCopy), param);
            }
        }

        readonly string copiedCoinName;

        public override IEnumerable<string> GetCopiedCoinNames()
        {
            return base.GetCopiedCoinNames().Concat(new[] { copiedCoinName });
        }

        IEnumerable<int> GetEmptySideAreaNos(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            var owner = selectedCoinData.CoinData.OwnerPlayerNo;
            var fieldData = duelData.FieldData;
            var areaNo = fieldData.GetContainCoinAreaNo(selectedCoinData);
            if (areaNo.HasValue &&
                duelEvent is AfterMovePlayer afterMovePlayer &&
                areaNo == afterMovePlayer.AreaNo &&
                afterMovePlayer.PlayerNo != owner)
            {
                foreach (int sideAreaNo in fieldData.GetSideAreaNosNotSafe(areaNo.Value).Where(areaNo => !fieldData.AreaDatas[areaNo].GetCoinsByOwner(owner).Any()))
                {
                    yield return sideAreaNo;
                }
            }
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return GetEmptySideAreaNos(duelData, selectedCoinData, duelEvent).Any();
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After duelEvent)
        {
            foreach (int sideAreaNo in GetEmptySideAreaNos(duelManager.DuelData, selectedCoinData, duelEvent))
            {
                var token = duelManager.DuelData.CreateCopy(selectedCoinData.CoinData.ID, selectedCoinData);

                duelManager.RegistDuelEventAction(new ActionAddCopyToSet()
                {
                    Target = token,
                    AreaNo = sideAreaNo
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
