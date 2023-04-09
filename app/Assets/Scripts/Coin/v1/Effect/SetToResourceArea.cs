using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Assets.Scripts.Duel.PlayerCondition;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class SetToResourceArea : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public SetToResourceArea(int value)
        {
            this.value = value;
        }

        readonly int value;

        public override string Explain
        {
            get
            {
                var param = new Dictionary<string, string>()
                {
                    { nameof(value), value.ToString()},
                };

                return GetEffectLocalizedString(nameof(SetToResourceArea), param);
            }
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return after is AfterMoveCoinsToSet afterMoveCoinsToSet &&
                afterMoveCoinsToSet.CoinMoveReason == Defines.CoinMoveReason.Set &&
                afterMoveCoinsToSet.SrcItems.Any(item => item.CoinID == selectedCoinData.CoinData.ID);
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                var afterMoveCoinsToSet = after as AfterMoveCoinsToSet;

                duelManager.RegistDuelEventAction(new ActionSetAppendResourceToArea()
                {
                    ExecutePlayerNo = selectedCoinData.CoinData.OwnerPlayerNo,
                    AreaNo = afterMoveCoinsToSet.DstAreaNo,
                    Value = value
                });
            }

            return UniTask.CompletedTask;
        }

        public override bool IsOnAreaEffect()
        {
            return false;
        }
    }
}
