using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class GetCondition : Scripts.Coin.Body.Support.NoneTarget
    {
        public GetCondition(string coinName, Duel.PlayerCondition.PlayerCondition playerCondition, Defines.ParticleType particleType)
            : base(coinName)
        {
            PlayerCondition = playerCondition;
            this.particleType = particleType;
        }

        public Duel.PlayerCondition.PlayerCondition PlayerCondition { get; }
        readonly Defines.ParticleType particleType;

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    int format = (0 < PlayerCondition.Value) ? 0 : 1;

                    var param = new Dictionary<string, string>()
                    {
                        { "name", PlayerCondition.GetDisplayName() },
                        { "value", Math.Abs(PlayerCondition.Value).ToString() },
                    };

                    yield return GetLocalizedString(nameof(GetCondition), format, param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var playerNo = actionItem.GetPlayerNo();
            duelManager.RegistDuelEventAction(new ActionEffectPlayer() { PlayerNo = playerNo, ParticleType = particleType });
            duelManager.RegistDuelEventAction(new ActionAddPlayerCondition() { PlayerNo = playerNo, PlayerCondition = PlayerCondition });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            var playerNo = actionItem.GetPlayerNo();

            // ここに来るのはSelfDynamo(自家発電)
            // 帯電が10以上になるなら使わない
            var pc = duelData.Players[playerNo].ConditionList.GetItem(PlayerCondition.InnerName);
            if (pc != null && duelData.GetConditionCount(PlayerCondition.InnerName) <= pc.Value + PlayerCondition.Value) return false;

            return true;
        }
    }
}
