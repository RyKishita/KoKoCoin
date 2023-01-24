using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Assets.Scripts.Utility;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Coin.v1.Body
{
    class MakeCopyOfSetCoin : Scripts.Coin.Body.Support.TargetSetCoin
    {
        public MakeCopyOfSetCoin(string coinName, Defines.CoinPosition dstCoinPosition)
            : base(coinName)
        {
            if (dstCoinPosition == Defines.CoinPosition.Field) throw new ArgumentException("設置にはできない");
            this.dstCoinPosition = dstCoinPosition;
        }

        readonly Defines.CoinPosition dstCoinPosition;

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    var coinposition = Defines.GetLocalizedString(dstCoinPosition);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(coinposition), coinposition },
                    };

                    yield return GetLocalizedString(nameof(MakeCopyOfSetCoin), param);
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
            return duelData.FieldData.GetAllAreaCoins()
                            .Where(scc => scc.CoinData.OwnerPlayerNo == selectedCoinData.CoinData.OwnerPlayerNo)
                            .Any();
        }

        public override bool IsMatchTarget(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData targetSelectedCoinData)
        {
            return targetSelectedCoinData.CoinData.OwnerPlayerNo == selectedCoinData.CoinData.OwnerPlayerNo;
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var coinID = (actionItem.SupportAction as Duel.SupportAction.SupportActionTargetSetCoin).CoinID;
            if (!coinID.HasValue)
            {
                duelManager.WriteLog(UnityEngine.LogType.Warning, "ロジックがおかしい");
                return UniTask.CompletedTask;
            }

            var areaNo = duelManager.DuelData.FieldData.GetContainCoinAreaNo(coinID.Value);
            if (!areaNo.HasValue)
            {
                duelManager.WriteLog(LogType.Warning, "設置されていないコインが対象になった");
                return UniTask.CompletedTask;
            }

            var target = duelManager.DuelData.FieldData.GetCoin(coinID.Value);
            var playerNo = actionItem.GetPlayerNo();

            var copyCoin = duelManager.DuelData.CreateCopy(actionItem.SelectedCoinData.CoinData.ID, target.CoinData.CoinName, playerNo);

            duelManager.RegistDuelEventAction(new ActionAddCopyToPlayer()
            {
                Target = copyCoin,
                CoinPosition = Defines.CoinPosition.Hand
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

            // サイズが大きいものを得る
            var coins = targetAreaNo.HasValue
                ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                : duelData.FieldData.GetAllAreaCoins().ToList();

            var result = coins
                .Where(scd => scd.CoinData.OwnerPlayerNo == playerNo)
                .OrderByDescending(scd => scd.GetCoin().Size)
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

            if (bUseForce)
            {
                Functions.WriteLog(LogType.Warning,
                    nameof(MakeCopyOfSetCoin),
                    actionItem.SelectedCoinData.CoinData.CoinName,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            actionItem.SupportAction = null;
            return false;
        }
    }
}
