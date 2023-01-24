using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class GuardToDraw : GuardTo
    {
        public GuardToDraw(int drawNum)
        {
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

                return GetEffectLocalizedString(nameof(GuardToDraw), param);
            }
        }

        readonly int num;

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterGuardNoDamage afterGuardNoDamage)
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

            return UniTask.CompletedTask;
        }
    }
}
