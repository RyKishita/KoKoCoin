using Assets.Scripts.Duel;
using Assets.Scripts.Duel.CoinStatus;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    abstract class AddValueStatusBy : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent, Scripts.Coin.Effect.IEffectAppendValuePrediction
    {
        public AddValueStatusBy(int value)
        {
            Value = value;
            status = new CoinStatusAppendValue() { Value = value };
        }

        public int Value { get; }

        readonly CoinStatusAppendValue status;

        public abstract bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after);

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                duelManager.RegistDuelEventAction(new ActionAddCoinStatus()
                {
                    CoinID = selectedCoinData.CoinData.ID,
                    CoinStatus = status
                });

                var particleType = 0 < Value ? Defines.ParticleType.Buf : Defines.ParticleType.Debuf;

                var coinPosition = duelManager.DuelData.GetCoinPosition(selectedCoinData.CoinData.ID);
                if (coinPosition == Defines.CoinPosition.Field)
                {
                    duelManager.RegistDuelEventAction(new ActionEffectSetCoin()
                    {
                        CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                        ParticleType = particleType
                    });
                }
                else
                {
                    duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                    {
                        PlayerNo= selectedCoinData.CoinData.OwnerPlayerNo,
                        CoinPosition = coinPosition,
                        ParticleType = particleType
                    });
                }
            }
            return UniTask.CompletedTask;
        }

        public override bool IsAdvantage(DuelData duelData)
        {
            return 0 < Value;
        }

        protected virtual bool IsMatchAddValueStatus(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            return false;
        }

        public int GetAppendValuePrediction(DuelData duelData, SelectedCoinData selectedCoinData, int baseValue)
        {
            return IsMatchAddValueStatus(duelData, selectedCoinData) ? Value : 0;
        }
    }
}
