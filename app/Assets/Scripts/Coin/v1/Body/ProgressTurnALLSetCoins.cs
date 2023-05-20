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
    class ProgressTurnALLSetCoins : Scripts.Coin.Body.Support.NoneTarget
    {
        public ProgressTurnALLSetCoins(string coinName, int turn)
            : base(coinName)
        {
            if (turn == 0) throw new ArgumentOutOfRangeException();
            ProgressTurnValue = turn;
        }

        public int ProgressTurnValue { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    int format = (0 < ProgressTurnValue) ? 0 : 1;
                    var value = Math.Abs(ProgressTurnValue).ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(value), value},
                    };

                    yield return GetLocalizedString(nameof(ProgressTurnALLSetCoins), format, param);
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
            return duelData.FieldData.GetAllAreaCoins().Any();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var coins = duelManager.DuelData
                        .FieldData
                        .GetAllAreaCoins()
                        .Select(scd => scd.CoinData.ID)
                        .ToList();

            duelManager.RegistDuelEventAction(new ActionAddTurn()
            {
                TargetCoinIDs = coins,
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

            var targetCoins = duelData.FieldData
                .GetAllAreaCoins()
                .ToList();

            var turnAdvantage = 0 < ProgressTurnValue;
            var ownerCount = targetCoins
                .Where(scd => scd.CoinData.OwnerPlayerNo == playerNo)
                .Count(scd => scd.GetCoinBody().IsAdvantageEffect_ProgressedTurn(duelData) == turnAdvantage);

            turnAdvantage = ProgressTurnValue < 0;
            var otherCount = targetCoins
                .Where(scd => scd.CoinData.OwnerPlayerNo != playerNo)
                .Count(scd => scd.GetCoinBody().IsAdvantageEffect_ProgressedTurn(duelData) == turnAdvantage);

            if (otherCount < ownerCount)
            {
                return true;
            }

            return false;
        }
    }
}
