using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ProgressTurnCoinAreaSetCoin : Scripts.Coin.Body.Support.Area
    {
        public ProgressTurnCoinAreaSetCoin(string coinName, int turn)
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
                    int format = (0 <= ProgressTurnValue) ? 0 : 1;

                    string value = Math.Abs(ProgressTurnValue).ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(value), value},
                    };

                    yield return GetLocalizedString(nameof(ProgressTurnCoinAreaSetCoin), format, param);
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

        public override List<int> GetTargetAreaNos(DuelData duelData)
        {
            return duelData.GetAreaNos().Where(areaNo => duelData.FieldData.AreaDatas[areaNo].Coins.Any()).ToList();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var areaNo = (actionItem.SupportAction as Duel.SupportAction.SupportActionTargetArea).AreaNo;
            var coins = duelManager.DuelData.FieldData.AreaDatas[areaNo].Coins.Select(scd => scd.CoinData.ID).ToList();

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
            int playerNo = actionItem.SelectedCoinData.CoinData.OwnerPlayerNo;

            // 中央に近いエリアを優先
            var areaNos = duelData.FieldData.GetAreaNosOrderCenter().ToList();

            var turnAdvantage = 0 < ProgressTurnValue;

            // ターンで変化するコインがあるエリア
            var resultAreaNos = areaNos.Where(areaNo => duelData.FieldData
                                                .AreaDatas[areaNo]
                                                .GetCoinsByOwner(playerNo)
                                                .Any(scd => scd.GetCoinBody().IsAdvantageEffect_ProgressedTurn(duelData) == turnAdvantage))
                            .ToList();
            if (resultAreaNos.Any())
            {
                actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetArea() { AreaNo = resultAreaNos.First() };
                return true;
            }

            if (bUseForce)
            {
                var reverseAreaNos = areaNos.ToList();
                reverseAreaNos.Reverse();
                resultAreaNos = reverseAreaNos.Where(areaNo => duelData.FieldData.AreaDatas[areaNo].Coins.Any()).ToList();
                if (resultAreaNos.Any())
                {
                    actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetArea() { AreaNo = resultAreaNos.First() };
                    return true;
                }

                Utility.Functions.WriteLog(UnityEngine.LogType.Warning,
                    nameof(ProgressTurnCoinAreaSetCoin),
                    actionItem.SelectedCoinData.CoinData.CoinName,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            return false;
        }
    }
}
