using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class DamageToGetCopy : DamageTo
    {
        public DamageToGetCopy(string copiedCoinName)
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

                return GetEffectLocalizedString(nameof(DamageToGetCopy), param);
            }
        }

        readonly string copiedCoinName;

        public override IEnumerable<string> GetCopiedCoinNames()
        {
            return base.GetCopiedCoinNames().Concat(new[] { copiedCoinName });
        }

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterDamageCoin afterDamageCoin)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

            duelManager.RegistDuelEventAction(new ActionEffectPlayer()
            {
                PlayerNo = playerNo,
                ParticleType = Defines.ParticleType.CreateCopy,
                IsWaitPlayParticle = true
            });

            var token = duelManager.DuelData.CreateCopy(selectedCoinData.CoinData.ID, copiedCoinName, playerNo);
            duelManager.RegistDuelEventAction(new ActionAddCopyToPlayer()
            {
                Target = token,
                CoinPosition = Defines.CoinPosition.Hand
            });

            return UniTask.CompletedTask;
        }
    }
}
