using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseSetToAddValueStatusByPlayerCondition : UseTo
    {
        public UseSetToAddValueStatusByPlayerCondition(bool bCoinOwner, string playerConditionInnerName, int value)
        {
            this.bCoinOwner = bCoinOwner;
            this.playerConditionInnerName = playerConditionInnerName;
            this.value = value;
        }

        readonly bool bCoinOwner;
        readonly string playerConditionInnerName;
        readonly int value;

        public override string Explain
        {
            get
            {
                int formatType = (0 < value) ? 0 : 1;
                string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                string conditionname = Duel.PlayerCondition.PlayerConditionDetail.GetLocalizedStringName(playerConditionInnerName);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { nameof(conditionname), conditionname },
                    { nameof(value), Math.Abs(value).ToString() },
                };

                return GetEffectLocalizedString(nameof(UseSetToAddValueStatusByPlayerCondition), formatType, param);
            }
        }

        protected override bool IsReceiveEventUseGuard(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return false; // 設置時効果なので防御時は常に一致しない
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterUse)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            var pc = duelManager.DuelData.Players[playerNo].ConditionList.GetItem(playerConditionInnerName);
            if (pc != null)
            {
                var playerConditionCount = pc.Value;

                duelManager.RegistDuelEventAction(new ActionEffectSetCoin()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    ParticleType = 0 < value ? Defines.ParticleType.Buf : Defines.ParticleType.Debuf
                });

                duelManager.RegistDuelEventAction(new ActionAddCoinStatus()
                {
                    CoinID = selectedCoinData.CoinData.ID,
                    CoinStatus = new Duel.CoinStatus.CoinStatusAppendValue() { Value = value * playerConditionCount }
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
