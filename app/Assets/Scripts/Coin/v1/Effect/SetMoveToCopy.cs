using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class SetMoveToCopy : SetMoveTo
    {
        public SetMoveToCopy(Defines.CoinPosition dstCoinPosition, Defines.CoinMoveReason? coinMoveReason, string copiedCoinName)
            : base(dstCoinPosition, coinMoveReason)
        {
            this.copiedCoinName = copiedCoinName;
        }

        public override string Explain
        {
            get
            {
                string trash = Defines.GetLocalizedString(dstCoinPosition);
                string coinname = CoinList.Instance.GetCoin(copiedCoinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(trash), trash},
                    { nameof(coinname), coinname},
                };

                return GetEffectLocalizedString(nameof(SetMoveToCopy), param);
            }
        }

        readonly string copiedCoinName;

        public override IEnumerable<string> GetCopiedCoinNames()
        {
            return base.GetCopiedCoinNames().Concat(new[] { copiedCoinName });
        }

        public override async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterMoveCoinsToPlayer afterMoveCoinsToPlayer)
        {
            var targetObject = duelManager.PlayerManagers[selectedCoinData.CoinData.OwnerPlayerNo].GetGameObject(Defines.CoinPosition.Trash);
            duelManager.SetFocus(targetObject);
            await duelManager.AnimateRaiseCoinAsync(selectedCoinData, targetObject);

            var srcItem = afterMoveCoinsToPlayer.SrcItems.First(item => item.CoinID == selectedCoinData.CoinData.ID);

            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

            var copySetCoin = duelManager.DuelData
                            .CreateCopy(selectedCoinData.CoinData.ID, copiedCoinName, playerNo)
                            .MakeSetCoin();

            duelManager.RegistDuelEventAction(new ActionAddCopyToSet()
            {
                Target = copySetCoin,
                AreaNo = srcItem.AreaNo.Value
            });
        }
    }
}
