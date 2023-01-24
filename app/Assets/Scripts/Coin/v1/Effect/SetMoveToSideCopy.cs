using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class SetMoveToSideCopy : SetMoveTo
    {
        public SetMoveToSideCopy(Defines.CoinPosition dstCoinPosition, Defines.CoinMoveReason? coinMoveReason, string copiedCoinName)
            : base(dstCoinPosition, coinMoveReason)
        {
            this.copiedCoinName = copiedCoinName;
        }

        public override string Explain
        {
            get
            {
                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);
                string trash = Defines.GetLocalizedString(dstCoinPosition);
                string coinname = CoinList.Instance.GetCoin(copiedCoinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(field), field},
                    { nameof(trash), trash},
                    { nameof(coinname), coinname},
                };

                return GetEffectLocalizedString(nameof(SetMoveToSideCopy), param);
            }
        }

        readonly string copiedCoinName;

        public override IEnumerable<string> GetCopiedCoinNames()
        {
            return base.GetCopiedCoinNames().Concat(new[] { copiedCoinName });
        }

        IEnumerable<int> GetEmptySideAreas(DuelData duelData, SelectedCoinData selectedCoinData, AfterMoveCoinsToPlayer afterMoveCoinsToPlayer)
        {
            var srcItem = afterMoveCoinsToPlayer.SrcItems
                            .Where(item => item.CoinID == selectedCoinData.CoinData.ID)
                            .Where(item => item.CoinPosition == Defines.CoinPosition.Field)
                            .First();
            var fieldData = duelData.FieldData;
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

            return fieldData.GetSideAreaNosNotSafe(srcItem.AreaNo.Value).Where(areaNo => !fieldData.AreaDatas[areaNo].GetCoinsByOwner(playerNo).Any());
        }

        protected override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, AfterMoveCoinsToPlayer afterMoveCoinsToPlayer)
        {
            return GetEmptySideAreas(duelData, selectedCoinData, afterMoveCoinsToPlayer).Any();
        }

        public override async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterMoveCoinsToPlayer afterMoveCoinsToPlayer)
        {
            var targetObject = duelManager.PlayerManagers[selectedCoinData.CoinData.OwnerPlayerNo].GetGameObject(Defines.CoinPosition.Trash);
            duelManager.SetFocus(targetObject);
            await duelManager.AnimateRaiseCoinAsync(selectedCoinData, targetObject);

            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

            foreach (int sideAreaNo in GetEmptySideAreas(duelManager.DuelData, selectedCoinData, afterMoveCoinsToPlayer))
            {
                var setCoin = duelManager.DuelData
                                .CreateCopy(selectedCoinData.CoinData.ID, copiedCoinName, playerNo)
                                .MakeSetCoin();

                duelManager.RegistDuelEventAction(new ActionAddCopyToSet()
                {
                    Target = setCoin,
                    AreaNo = sideAreaNo
                });
            }
        }
    }
}
