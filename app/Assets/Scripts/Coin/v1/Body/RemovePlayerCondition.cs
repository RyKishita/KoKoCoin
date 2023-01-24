using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class RemovePlayerCondition : Scripts.Coin.Body.Support.NoneTarget
    {
        public RemovePlayerCondition(string coinName, bool bCoinOwner, Duel.PlayerCondition.PlayerCondition targetPlayerCondtion)
            : base(coinName)
        {
            if (targetPlayerCondtion == null) throw new ArgumentNullException(nameof(targetPlayerCondtion));
            this.bCoinOwner = bCoinOwner;
            TargetPlayerCondtion = targetPlayerCondtion;
        }

        readonly bool bCoinOwner;
        public Duel.PlayerCondition.PlayerCondition TargetPlayerCondtion { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string player = Defines.GetLocalizedString(bCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                    string name = TargetPlayerCondtion.GetDisplayName();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(player), player },
                        { nameof(name), name},
                    };

                    yield return GetLocalizedString(nameof(RemovePlayerCondition), param);
                }
;
                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;
            var owner = selectedCoinData.CoinData.OwnerPlayerNo;
            return duelData.Players
                .Where(player => (player.PlayerNo == owner) == bCoinOwner)
                .Where(player => player.ConditionList.Has(TargetPlayerCondtion))
                .Any();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var playerNo = actionItem.GetPlayerNo();
            foreach (var player in duelManager.DuelData
                                    .Players
                                    .Where(player => (player.PlayerNo == playerNo) == bCoinOwner)
                                    .Where(player => player.ConditionList.Has(TargetPlayerCondtion)))
            {
                duelManager.RegistDuelEventAction(new ActionRemovePlayerCondition()
                {
                    PlayerNo = player.PlayerNo,
                    PlayerConditionName = TargetPlayerCondtion.InnerName
                });
                duelManager.RegistDuelEventAction(new ActionEffectPlayer() { PlayerNo = player.PlayerNo, ParticleType = Defines.ParticleType.Cure });
            }
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            // 使いたいから入れているはずなのでチェックせず実行
            return true;
        }
    }
}
