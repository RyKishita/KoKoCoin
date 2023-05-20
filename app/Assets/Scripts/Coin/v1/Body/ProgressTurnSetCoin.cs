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
    class ProgressTurnSetCoin : Scripts.Coin.Body.Support.TargetSetCoin
    {
        public ProgressTurnSetCoin(string coinName, int turn)
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

                    var settedcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoin);
                    var value = Math.Abs(ProgressTurnValue).ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(settedcoin), settedcoin },
                        { nameof(value), value},
                    };

                    yield return GetLocalizedString(nameof(ProgressTurnSetCoin), format, param);
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

        public override bool IsMatchTarget(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData targetSelectedCoinData)
        {
            return true;
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var coinID = (actionItem.SupportAction as Duel.SupportAction.SupportActionTargetSetCoin).CoinID;
            if (!coinID.HasValue)
            {
                duelManager.WriteLog(UnityEngine.LogType.Warning, "ロジックがおかしい");
                return UniTask.CompletedTask;
            }

            duelManager.RegistDuelEventAction(new ActionAddTurn()
            {
                TargetCoinIDs = new List<int>() { coinID.Value },
                Turn = ProgressTurnValue,
                Reason = Defines.AddTurnReasonEnum.Effect
            });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            int? targetAreaNo = null;
            if (actionItem.SupportAction is Duel.SupportAction.SupportActionTargetSetCoin supportActionTargetSetCoin)
            {
                targetAreaNo = supportActionTargetSetCoin.AreaNo;
            }

            var playerNo = actionItem.GetPlayerNo();

            var coins = targetAreaNo.HasValue
                ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                : duelData.FieldData.GetAllAreaCoinsOrderCenter().ToList();

            var result = coins
                .Where(scd =>
                {
                    var turnAdvantage = (scd.CoinData.OwnerPlayerNo == playerNo) == (0 < ProgressTurnValue);
                    return scd.GetCoinBody().IsAdvantageEffect_ProgressedTurn(duelData) == turnAdvantage;
                }).FirstOrDefault();
            if (result != null)
            {
                actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetSetCoin()
                {
                    AreaNo = targetAreaNo ?? duelData.FieldData.GetContainCoinAreaNo(result) ?? 0,
                    CoinID = result.CoinData.ID
                };
                return true;
            }

            if (bUseForce)
            {
                result = duelData.FieldData
                    .GetAllAreaCoinsOrderOutSide()
                    .FirstOrDefault();
                if (result != null)
                {
                    actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetSetCoin()
                    {
                        AreaNo = targetAreaNo ?? duelData.FieldData.GetContainCoinAreaNo(result) ?? 0,
                        CoinID = result.CoinData.ID
                    };
                    return true;
                }

                Utility.Functions.WriteLog(UnityEngine.LogType.Warning,
                    nameof(ProgressTurnSetCoin),
                    actionItem.SelectedCoinData.CoinData.CoinName,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            actionItem.SupportAction = null;
            return false;
        }
    }
}
