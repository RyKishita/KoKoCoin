using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToGetCoin : UseTo
    {
        public UseToGetCoin(string coinName)
        {
            this.coinName = coinName;
        }

        public override string Explain
        {
            get
            {
                string stockcoin = Defines.GetLocalizedString(Defines.CoinPosition.Stock);
                string coinname = CoinList.Instance.GetCoin(coinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(stockcoin), stockcoin},
                    { nameof(coinname), coinname},
                };

                return GetEffectLocalizedString(nameof(UseToGetCoin), param);
            }
        }

        readonly string coinName;

        protected override bool IsReceiveEventUse(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!base.IsReceiveEventUse(duelData, selectedCoinData, duelEvent)) return false;

            var stock = duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].Stock;
            return stock.IsContains(coinName);
        }

        protected override bool IsReceiveEventUseGuard(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            if (!base.IsReceiveEventUseGuard(duelData, selectedCoinData, duelEvent)) return false;

            var stock = duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].Stock;
            return stock.IsContains(coinName);
        }

        void ReceiveEventBody(DuelManager duelManager, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

            var coin = duelManager.DuelData.Players[playerNo].Stock.Items.First(cd => cd.CoinName == coinName);

            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinIDs = new List<int>() { coin.ID },
                DstCoinPosition = Defines.CoinPosition.Hand
            });

            duelManager.RegistDuelEventAction(new ActionShuffleStock()
            {
                PlayerNo = playerNo
            });
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterUse)
        {
            ReceiveEventBody(duelManager, selectedCoinData);
            return UniTask.CompletedTask;
        }

        protected override UniTask ReceiveEventUseGuardBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUseGuard afterEndUseGuard)
        {
            ReceiveEventBody(duelManager, selectedCoinData);
            return UniTask.CompletedTask;
        }
    }
}
