using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class ClearPlayerCondition : Scripts.Coin.Body.Support.NoneTarget
    {
        public ClearPlayerCondition(string coinName)
            : base(coinName)
        {

        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string abnormalstate = Defines.GetLocalizedString(Defines.StringEnum.AbnormalState);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(abnormalstate), abnormalstate},
                    };

                    yield return GetLocalizedString(nameof(ClearPlayerCondition), param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            return !duelData.Players[playerNo].ConditionList.IsEmpty();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var playerNo = actionItem.GetPlayerNo();
            duelManager.RegistDuelEventAction(new ActionClearPlayerCondition() { PlayerNo = playerNo });
            duelManager.RegistDuelEventAction(new ActionEffectPlayer() { PlayerNo = playerNo, ParticleType = Defines.ParticleType.Cure });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            // 帯電デッキなら帯電を持っておらず他の物を持っているなら使うというようにしたいが、
            // テーマは分からないので考慮しない
            return true;
        }
    }
}
