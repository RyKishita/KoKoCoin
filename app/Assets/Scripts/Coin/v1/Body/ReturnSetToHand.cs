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
    class ReturnSetToHand : Scripts.Coin.Body.Support.TargetSetCoin
    {
        public ReturnSetToHand(string coinName)
            : base(coinName)
        {

        }

        readonly Defines.CoinPosition dstCoinPosition = Defines.CoinPosition.Hand;

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string settedcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);
                    string dst = Defines.GetLocalizedString(Defines.CoinPosition.Hand);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(settedcoin), settedcoin },
                        { nameof(dst), dst },
                    };

                    yield return GetLocalizedString(nameof(ReturnSetToHand), param);
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
                duelManager.WriteLog(LogType.Warning, "ロジックがおかしい");
                return UniTask.CompletedTask;
            }

            var areaNo = duelManager.DuelData.FieldData.GetContainCoinAreaNo(coinID.Value);
            if (!areaNo.HasValue)
            {
                duelManager.WriteLog(LogType.Warning, "設置されていないコインが対象になった");
                return UniTask.CompletedTask;
            }

            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinMoveReason = Defines.CoinMoveReason.Effect,
                CoinIDs = new List<int>() { coinID.Value },
                DstCoinPosition = dstCoinPosition
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

            // 手持ちに戻したいものとは？→使用時点で役目を果たしたコイン

            Func<Scripts.Coin.Effect.IEffect, bool> isTargetEffect = effect =>
            {
                switch (effect)
                {
                    case Effect.AddConditionToAreaBySet:
                    case Effect.AddConditionToPlayerBySet:
                    case Effect.SetToResourceArea:
                    case Effect.SetToSideCopy:
                    case Effect.UseTo:
                        return effect.IsAdvantage(duelData);
                    default:
                        return false;
                }
            };

            var coins = targetAreaNo.HasValue
                ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                : duelData.FieldData.GetAllAreaCoins().ToList();

            var result = coins
                .Where(scd => scd.CoinData.OwnerPlayerNo == playerNo)
                .Where(scd => scd.GetCoinBody().Effects.Any(effect => isTargetEffect(effect)))
                .OrderBy(scd => scd.GetCoin().Size)
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
                coins = targetAreaNo.HasValue
                    ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                    : duelData.FieldData.GetAllAreaCoinsOrderOutSide().ToList();

                result = coins
                    .Where(scd => scd.CoinData.OwnerPlayerNo == playerNo)
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

                Functions.WriteLog(LogType.Warning,
                    nameof(ReturnSetToHand),
                    actionItem.SelectedCoinData.CoinData.CoinName,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            actionItem.SupportAction = null;
            return false;
        }
    }
}
