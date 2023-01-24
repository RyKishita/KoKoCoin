using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ProgressTurnHandCoinsByPlayer : Scripts.Coin.Body.Support.NoneTarget
    {
        public ProgressTurnHandCoinsByPlayer(string coinName, bool bCoinOwner, int turn)
            : base(coinName)
        {
            if (turn == 0) throw new ArgumentOutOfRangeException();
            this.IsTargetCoinOwner = bCoinOwner;
            ProgressTurnValue = turn;
        }

        public bool IsTargetCoinOwner { get; }
        public int ProgressTurnValue { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    int formatType = (0 < ProgressTurnValue) ? 0 : 1;
                    if (!IsTargetCoinOwner)
                    {
                        formatType += 2;
                    }

                    string value = Math.Abs(ProgressTurnValue).ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(value), value},
                    };

                    yield return GetLocalizedString(nameof(ProgressTurnHandCoinsByPlayer), formatType, param);
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
                .Where(player => (player.PlayerNo == owner) == IsTargetCoinOwner)
                .Where(player => !player.Hand.IsEmpty())
                .Any();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var playerNo = actionItem.GetPlayerNo();

            var targetCoins = IsTargetCoinOwner
                ? duelManager.DuelData
                    .Players[playerNo]
                    .Hand
                    .Items
                : duelManager.DuelData
                    .Players
                    .Where(player => player.PlayerNo != playerNo)
                    .SelectMany(player => player.Hand.Items);

            duelManager.RegistDuelEventAction(new ActionAddTurn()
            {
                TargetCoinDataIDs = targetCoins.Select(cc => cc.ID).ToList(),
                Turn = ProgressTurnValue,
                Reason = Defines.AddTurnReasonEnum.Effect
            });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            var playerNo = actionItem.GetPlayerNo();

            if (IsTargetCoinOwner)
            {
                var turnAdvantage = 0 < ProgressTurnValue;
                if (duelData.Players[playerNo]
                    .Hand
                    .Items
                    .Any(cd => cd.GetCoin().Bodies.Any(body => body.IsAdvantageEffect_ProgressedTurn(duelData) == turnAdvantage)))
                {
                    return true;
                }
            }
            else
            {
                var turnAdvantage = ProgressTurnValue < 0;
                if (duelData.Players
                    .Where(player => player.PlayerNo != playerNo)
                    .SelectMany(player => player.Hand.Items)
                    .Any(cd => cd.GetCoin().Bodies.Any(body => body.IsAdvantageEffect_ProgressedTurn(duelData) == turnAdvantage)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
