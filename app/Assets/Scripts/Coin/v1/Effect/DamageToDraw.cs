using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class DamageToDraw : DamageTo
    {
        public DamageToDraw(int drawNum)
        {
            if (drawNum <= 0) throw new ArgumentOutOfRangeException(nameof(drawNum));
            num = drawNum;
        }

        public override string Explain
        {
            get
            {
                var param = new Dictionary<string, string>()
                {
                    { nameof(num), num.ToString()},
                };

                return GetEffectLocalizedString(nameof(DamageToDraw), param);
            }
        }

        readonly int num;

        public override bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return base.IsReceiveEvent(duelData, selectedCoinData, duelEvent) &&
                    !duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].Stock.IsEmpty();
        }

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterDamageCoin afterDamageCoin)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            var ownerPlayer = duelManager.DuelData.Players[playerNo];

            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = playerNo,
                ParticleType = Defines.ParticleType.CoinDraw
            });

            int take = Math.Min(ownerPlayer.Stock.Items.Count, num);

            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinIDs = ownerPlayer.Stock.Items.Take(take).Select(coin => coin.ID).ToList(),
                DstCoinPosition = Defines.CoinPosition.Hand,
                CoinMoveReason = Defines.CoinMoveReason.Effect
            });

            return UniTask.CompletedTask;
        }
    }
}
