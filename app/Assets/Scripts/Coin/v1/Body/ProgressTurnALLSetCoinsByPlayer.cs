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
    class ProgressTurnALLSetCoinsByPlayer : Scripts.Coin.Body.Support.NoneTarget
    {
        public ProgressTurnALLSetCoinsByPlayer(string coinName, bool bCoinOwner, int turn)
            : base(coinName)
        {
            if (turn == 0) throw new ArgumentOutOfRangeException();
            IsTargetCoinOwner = bCoinOwner;
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

                    string settedcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoin);
                    string value = Math.Abs(ProgressTurnValue).ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(settedcoin), settedcoin},
                        { nameof(value), value},
                    };

                    yield return GetLocalizedString(nameof(ProgressTurnALLSetCoinsByPlayer), formatType, param);
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
            var owner = selectedCoinData.CoinData.OwnerPlayerNo;
            return duelData.FieldData
                .GetAllAreaCoins()
                .Where(scc => (scc.CoinData.OwnerPlayerNo == owner) == IsTargetCoinOwner)
                .Any();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var playerNo = actionItem.GetPlayerNo();
            var coins = duelManager.DuelData.FieldData
                        .GetAllAreaCoins()
                        .Where(scd => (scd.CoinData.OwnerPlayerNo == playerNo) == IsTargetCoinOwner)
                        .Select(scd => scd.CoinData.ID)
                        .ToList();

            duelManager.RegistDuelEventAction(new ActionAddTurn()
            {
                TargetCoinDataIDs = coins,
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
                if (duelData.FieldData
                    .GetAllAreaCoins()
                    .Where(scd => scd.CoinData.OwnerPlayerNo == playerNo)
                    .Any(scd => scd.GetCoinBody().IsAdvantageEffect_ProgressedTurn(duelData) == turnAdvantage))
                {
                    return true;
                }
            }
            else
            {
                var turnAdvantage = ProgressTurnValue < 0;
                if (duelData.FieldData
                    .GetAllAreaCoins()
                    .Where(scd => scd.CoinData.OwnerPlayerNo != playerNo)
                    .Any(scd => scd.GetCoinBody().IsAdvantageEffect_ProgressedTurn(duelData) == turnAdvantage))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
