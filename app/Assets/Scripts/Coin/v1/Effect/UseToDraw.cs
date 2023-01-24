using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToDraw : UseTo
    {
        public UseToDraw(int drawNum)
        {
            num = drawNum;
        }

        public override string Explain
        {
            get
            {
                string stockcoin = Defines.GetLocalizedString(Defines.CoinPosition.Stock);

                var param = new Dictionary<string, string>()
                {
                    { nameof(stockcoin), stockcoin},
                    { nameof(num), num.ToString()},
                };

                return GetEffectLocalizedString(nameof(UseToDraw), param);
            }
        }

        readonly int num;

        void ReceiveEventBody(DuelManager duelManager, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            var player = duelManager.DuelData.Players[playerNo];

            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = playerNo,
                ParticleType = Defines.ParticleType.CoinDraw
            });

            int take = Math.Min(player.Stock.Items.Count, num);
            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinIDs = player.Stock.Items.Take(take).Select(coin => coin.ID).ToList(),
                DstCoinPosition = Defines.CoinPosition.Hand,
                CoinMoveReason = Defines.CoinMoveReason.Effect
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
