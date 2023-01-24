using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Assets.Scripts.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Coin.v1.Body
{
    class SwapSetBySideArea : Scripts.Coin.Body.Support.AreaSide
    {
        public SwapSetBySideArea(string coinName)
            : base(coinName)
        {

        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                yield return GetLocalizedString(nameof(SwapSetBySideArea));

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;
            return duelData.FieldData
                .GetAllAreaCoins()
                .Where(scd => scd.CoinData.OwnerPlayerNo == selectedCoinData.CoinData.OwnerPlayerNo)
                .Any();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var playerNo = actionItem.GetPlayerNo();

            var result = (actionItem.SupportAction as Duel.SupportAction.SupportActionTargetAreaAndSide);
            int selectedAreaNo1 = result.AreaNo1;
            int selectedAreaNo2 = result.AreaNo2;

            var area1 = duelManager.DuelData.FieldData.AreaDatas[selectedAreaNo1];
            var area2 = duelManager.DuelData.FieldData.AreaDatas[selectedAreaNo2];

            var ownerCoinList1 = area1.GetCoinsByOwner(playerNo).Select(scd => scd.CoinData.ID).ToList();
            var ownerCoinList2 = area2.GetCoinsByOwner(playerNo).Select(scd => scd.CoinData.ID).ToList();

            duelManager.RegistDuelEventAction(new ActionEffectArea() { AreaNo = selectedAreaNo1 });
            duelManager.RegistDuelEventAction(new ActionEffectArea() { AreaNo = selectedAreaNo2 });
            duelManager.RegistDuelEventAction(new ActionMoveCoinsToSet()
            {
                CoinIDs = ownerCoinList1,
                DstAreaNo = selectedAreaNo2,
                CoinMoveReason = Defines.CoinMoveReason.Effect
            });
            duelManager.RegistDuelEventAction(new ActionMoveCoinsToSet()
            {
                CoinIDs = ownerCoinList2,
                DstAreaNo = selectedAreaNo1,
                CoinMoveReason = Defines.CoinMoveReason.Effect
            });
            return UniTask.CompletedTask;
        }

        static int? CheckSide(DuelData duelData, int playerNo, int areaNo)
        {
            int damage = duelData.CalcSetAttackTotalDamage(playerNo, areaNo);
            int centerNo = duelData.FieldData.AreaCenterNo;
            int areaDistance = Math.Abs(areaNo - centerNo);
            foreach (var sideAreaNo in duelData.FieldData.GetSideAreaNosNotSafe(areaNo))
            {
                int damageSide = duelData.CalcSetAttackTotalDamage(playerNo, sideAreaNo);
                if (damage == damageSide) continue;

                int sideAreaDistance = Math.Abs(sideAreaNo - centerNo);
                if (areaDistance == sideAreaDistance) continue;

                if ((damage < damageSide) == (areaDistance < sideAreaDistance))
                {
                    return sideAreaNo;
                }
            }
            return null;
        }

        static int? CheckSideForce(DuelData duelData, int areaNo)
        {
            return duelData.FieldData.GetSideAreaNosNotSafe(areaNo).FirstOrDefault();
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, ActionItem.TargetAreaStep targetAreaStep, bool bUseForce)
        {
            var playerNo = actionItem.GetPlayerNo();

            switch (targetAreaStep)
            {
                case ActionItem.TargetAreaStep.MainArea:
                    foreach (var areaNo in duelData.FieldData
                                            .GetAreaNosOrderCenter()
                                            .Where(areaNo => duelData.FieldData.AreaDatas[areaNo].GetCoinsByOwner(playerNo).Any()))
                    {
                        var sideAreaNo = CheckSide(duelData, playerNo, areaNo);
                        if (sideAreaNo.HasValue)
                        {
                            actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetAreaAndSide()
                            {
                                AreaNo1 = areaNo,
                                AreaNo2 = sideAreaNo.Value
                            };
                            return true;
                        }
                    }
                    break;
                case ActionItem.TargetAreaStep.SideArea:
                    if (actionItem.SupportAction is Duel.SupportAction.SupportActionTargetAreaAndSide supportActionTargetAreaAndSide)
                    {
                        var sideAreaNo = CheckSide(duelData, playerNo, supportActionTargetAreaAndSide.AreaNo1);
                        if (sideAreaNo.HasValue)
                        {
                            supportActionTargetAreaAndSide.AreaNo2 = sideAreaNo.Value;
                            return true;
                        }
                    }
                    else
                    {
                        Functions.WriteLog(LogType.Warning,
                            nameof(SwapSetBySideArea),
                            actionItem.SelectedCoinData.CoinData.CoinName,
                            "一つ目エリア選択がされていない"
                        );
                    }
                    break;
            }
            if (bUseForce)
            {
                switch (targetAreaStep)
                {
                    case ActionItem.TargetAreaStep.MainArea:
                        foreach (var areaNo in duelData.FieldData
                                                .GetAreaNosOrderOutSide()
                                                .Where(areaNo => duelData.FieldData.AreaDatas[areaNo].GetCoinsByOwner(playerNo).Any()))
                        {
                            var sideAreaNo = CheckSideForce(duelData, areaNo);
                            if (sideAreaNo.HasValue)
                            {
                                actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetAreaAndSide()
                                {
                                    AreaNo1 = areaNo,
                                    AreaNo2 = sideAreaNo.Value
                                };
                                return true;
                            }
                        }
                        break;
                    case ActionItem.TargetAreaStep.SideArea:
                        if (actionItem.SupportAction is Duel.SupportAction.SupportActionTargetAreaAndSide supportActionTargetAreaAndSide)
                        {
                            var sideAreaNo = CheckSideForce(duelData, supportActionTargetAreaAndSide.AreaNo1);
                            if (sideAreaNo.HasValue)
                            {
                                supportActionTargetAreaAndSide.AreaNo2 = sideAreaNo.Value;
                                return true;
                            }
                        }
                        else
                        {
                            Functions.WriteLog(LogType.Warning, 
                                nameof(SwapSetBySideArea),
                                actionItem.SelectedCoinData.CoinData.CoinName,
                                "一つ目エリア選択がされていない"
                            );
                        }
                        break;
                }
                Functions.WriteLog(LogType.Warning,
                    nameof(SwapSetBySideArea),
                    actionItem.SelectedCoinData.CoinData.CoinName,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            return false;
        }
    }
}
