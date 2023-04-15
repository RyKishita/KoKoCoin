using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Duel
{
    class AutoPlayerInputer
    {
        public AutoPlayerInputer(DuelDataManager duelDataManager)
        {
            this.duelDataManager = duelDataManager;
        }

        readonly DuelDataManager duelDataManager;

        DuelData duelData => duelDataManager.DuelData;

        ActionItem SelectSupport(List<CoinData> handCoinDatas, IEnumerable<SelectedCoinData> uses)
        {
            WriteLog(nameof(SelectSupport));

            // サポートコイン毎の使用優先順位。現状は先頭から使えるものを使っている
            foreach (var useItem in uses.Where(item => item.IsUsable(duelData)))
            {
                var actionItem = SelectSupport(useItem, handCoinDatas, false);
                if (actionItem != null) return actionItem;
            }
            if (duelData.IsMustUse())
            {
                foreach (var useItem in uses.Where(item => item.IsUsable(duelData)))
                {
                    var actionItem = SelectSupport(useItem, handCoinDatas, true);
                    if (actionItem != null) return actionItem;
                }
            }
            return null;
        }

        public ActionItem SelectSupport_CoinOwnerCoins(ActionItem actionItem, Defines.CoinPosition coinPosition, bool bUseForce)
        {
            if (actionItem.SelectedCoinData.GetCoinBody() is Coin.Body.Support.CoinOwnerCoins coinOwnerCoins)
            {
                if (coinOwnerCoins.SelectAuto(duelData, actionItem, coinPosition, bUseForce)) return actionItem;
            }
            return null;
        }

        public ActionItem SelectSupport_TargetSetCoin(ActionItem actionItem, bool bUseForce)
        {
            if (actionItem.SelectedCoinData.GetCoinBody() is Coin.Body.Support.TargetSetCoin targetSetCoin)
            {
                if (targetSetCoin.SelectAuto(duelData, actionItem, bUseForce)) return actionItem;
            }

            return null;
        }

        ActionItem SelectSupport(SelectedCoinData selectedCoinData, IEnumerable<CoinData> handCoinDatas, bool bUseForce)
        {
            var extraCostCoinList = new List<CoinData>();
            if (selectedCoinData.GetCoinBody().IsQueryExtraCost())
            {
                extraCostCoinList = QueryNeedCoin(handCoinDatas, selectedCoinData);
                if (extraCostCoinList == null) return null;
            }

            var actionItem = new ActionItem()
            {
                SelectedCoinData = selectedCoinData,
                ExtraCostCoins = extraCostCoinList
            };

            switch (actionItem.SelectedCoinData.GetCoinBody())
            {
                case Coin.Body.Support.NoneTarget noneTarget:
                    return noneTarget.SelectAuto(duelData, actionItem, bUseForce) ? actionItem : null;
                case Coin.Body.Support.Area area:
                    return SelectSupport_Area(actionItem, bUseForce);
                case Coin.Body.Support.AreaSide _:
                    return SelectSupport_AreaSide(actionItem, ActionItem.TargetAreaStep.MainArea, bUseForce);
                case Coin.Body.Support.TargetSetCoin _:
                    return SelectSupport_TargetSetCoin(actionItem, bUseForce);
                case Coin.Body.Support.CoinOwnerCoins coinOwnerCoins:
                    return coinOwnerCoins.SelectAuto(duelData, actionItem, null, bUseForce) ? actionItem : null;
                default:
                    WriteLog(LogType.Warning,
                        nameof(SelectSupport),
                        actionItem.SelectedCoinData.CoinData.CoinName,
                        "未実装のターゲット"
                    );
                    break;
            }
            return null;
        }

        public ActionItem SelectSupport_Area(ActionItem actionItem, bool bUseForce)
        {
            if (actionItem.SelectedCoinData.GetCoinBody() is Coin.Body.Support.Area area)
            {
                if (area.SelectAuto(duelData, actionItem, bUseForce)) return actionItem;
            }
            return null;
        }

        public ActionItem SelectGuard(Player targetPlayer, ActionItem directAttackActionItem)
        {
            var usableGuardCoins = targetPlayer.Hand
                                .GetSeparateItems()
                                .Where(scc => scc.GetCoinBody().IsCoinType(Defines.CoinType.Guard))
                                .Where(scc => scc.IsUsable(duelData));
            if (!duelData.IsMustUse())
            {
                var mustUseCoins = usableGuardCoins.Where(scd => scd.IsUseForce()).ToList();
                if (mustUseCoins.Any())
                {
                    usableGuardCoins = mustUseCoins;
                }
            }

            var guardItems = usableGuardCoins
                                .Select(guardCoinItem =>
                                {
                                    return new
                                    {
                                        guardCoinItem,
                                        protection = guardCoinItem.GetGuardProtection(duelData, directAttackActionItem.SelectedCoinData)
                                    };
                                });

            int damage = directAttackActionItem.SelectedCoinData.GetDirectAttackDamage(duelData);

            // 優先度
            // 1,ダメージを全て抑えることが出来るコインの中で軽減量が一番低いもの
            // 2.軽減量が一番高いもの
            var result = guardItems
                            .Where(item => damage <= item.protection)
                            .FirstOrDefault()
                        ?? guardItems
                            .OrderByDescending(item => item.protection)
                            .FirstOrDefault();
            if (result == null) return null;

            return new ActionItem() { SelectedCoinData = result.guardCoinItem };
        }

        public ActionItem SelectSupport_AreaSide(ActionItem actionItem, ActionItem.TargetAreaStep targetAreaStep, bool bUseForce)
        {
            if (actionItem.SelectedCoinData.GetCoinBody() is Coin.Body.Support.AreaSide areaSide)
            {
                if (areaSide.SelectAuto(duelData, actionItem, targetAreaStep, bUseForce)) return actionItem;
            }
            return null;
        }

        public int SelectMoveArea(int playerNo, List<int> movableAreaNos)
        {
            int result;
            switch (movableAreaNos.Count)
            {
                case 0:
                    result = duelData.Players[playerNo].CurrentAreaNo;
                    break;
                case 1:
                    result = movableAreaNos[0];
                    break;
                default:
                    {
                        // 初期値として、一番中央に近い場所
                        int centerNo = duelData.FieldData.AreaCenterNo;
                        result = movableAreaNos.OrderBy(p => Math.Abs(p - centerNo)).First();
                        int damage = duelData.CalcSetAttackTotalDamageByOtherTeams(playerNo, result);

                        int getSafeDiceCount(int checkAreaNo)
                        {
                            return duelData.GetPlayerMovableAllAreaNos(playerNo, checkAreaNo)
                                    .GroupBy(areaNo => Math.Abs(areaNo - checkAreaNo))
                                    .Count(g => g.Any(areaNo => 0 == duelData.CalcSetAttackTotalDamageByOtherTeams(playerNo, areaNo)));
                        }

                        bool checkAdvantageAreaNo(int checkAreaNo, int currentAreaNo)
                        {
                            int checkAreaCount = getSafeDiceCount(checkAreaNo);
                            int currentAreaCount = getSafeDiceCount(currentAreaNo);

                            return checkAreaCount == currentAreaCount
                                ? Math.Abs(checkAreaNo - centerNo) < Math.Abs(currentAreaNo - centerNo)
                                : currentAreaCount < checkAreaCount;
                        }

                        // それ以外の場所でより有利なところを探す
                        // ダメージが少ない、もしくはダメージが同じならその後が有利な方
                        foreach (int areaNo in movableAreaNos.Where(no => no != result).ToList())
                        {
                            int d = duelData.CalcSetAttackTotalDamageByOtherTeams(playerNo, areaNo);

                            WriteLog(nameof(SelectMoveArea), result, damage, areaNo, d);

                            if (d < damage ||
                                d == damage && checkAdvantageAreaNo(areaNo, result))
                            {
                                result = areaNo;
                                damage = d;

                                WriteLog(nameof(SelectMoveArea), "update");
                            }
                        }
                    }
                    break;
            }

            return result;
        }

        ActionItem MakeActionItem(Player player, SelectedCoinData scd)
        {
            var coinDatas = player.Hand.Items;

            var extraCostList = new List<CoinData>();
            if (!duelData.IsNoNeed() && scd.GetCoinBody().IsQueryExtraCost())
            {
                extraCostList = QueryNeedCoin(coinDatas, scd);
                if (extraCostList == null) return null;
            }

            return new ActionItem() { SelectedCoinData = scd, ExtraCostCoins = extraCostList };
        }

        ActionItem GetUseCoinInfo(Player player, IEnumerable<SelectedCoinData> scds)
        {
            foreach (var scd in scds)
            {
                var actionItem = MakeActionItem(player, scd);
                if (actionItem != null) return actionItem;
            }
            return null;
        }

        public static CoinData QueryMostUselessCoin(IEnumerable<CoinData> ccs)
        {
            #region 環境コインが複数枚あるならいずれかから
            {
                var coins = ccs.Where(cc => cc.GetCoin().Bodies.Where(body => body is Coin.Body.Set.Environment.IEnvironment).Any()).ToList();
                if (2 <= coins.Count)
                {
                    // サイズの大きいものを残す
                    return coins.OrderBy(cc => cc.GetCoin().Size).First();
                }
            }
            #endregion

            #region サポートコインが複数枚あるならいずれかから
            {
                var coins = ccs.Where(cc => cc.GetCoin().Bodies.Where(body => body is Coin.Body.Support.Core).Any()).ToList();
                if (2 <= coins.Count)
                {
                    // サイズの大きいものを残す
                    return coins.OrderBy(cc => cc.GetCoin().Size).First();
                }
            }
            #endregion

            #region 防御コインが複数枚あるならいずれかから
            {
                var coins = ccs.Where(cc => cc.GetCoin().Bodies.Where(body => body is Coin.Body.Guard.Core).Any()).ToList();
                if (2 <= coins.Count)
                {
                    // サイズの大きいものを残す
                    return coins.OrderBy(cc => cc.GetCoin().Size).First();
                }
            }
            #endregion

            // 決まらなかったら一番大きいものからランダム
            var largeGroup = ccs.GroupBy(item => item.GetCoin().Size).OrderByDescending(group => group.Key).First();
            var largerCoinItems = largeGroup.ToList();
            return UserDataManager.Instance.QueryRandom(largerCoinItems);
        }

        static CoinData QueryMostUselessCoin(IEnumerable<CoinData> coinDatas, int maxHandCoinSize)
        {
            if (!coinDatas.Any()) return null;

            #region 制限を超えている分と同じサイズのコインを優先して選ぶ

            int totalSize = coinDatas.Sum(cd => cd.GetCoin().Size);
            int overSize = totalSize - maxHandCoinSize;
            if (0 < overSize)
            {
                var overSizeCoins = coinDatas.Where(cd => cd.GetCoin().Size == overSize).ToList();
                if (overSizeCoins.Any())
                {
                    return QueryMostUselessCoin(overSizeCoins);
                }
            }

            #endregion

            return QueryMostUselessCoin(coinDatas);
        }

        public CoinData SelectHandBySupport(CoinDataList coinDataList)
        {
            if (coinDataList.IsEmpty()) return null;

            return duelData.HasHandCoinSizeLimit()
                ? QueryMostUselessCoin(coinDataList.Items, duelData.GetMaxHandCoinSize())
                : QueryMostUselessCoin(coinDataList.Items);
        }

        static List<CoinData> QueryNeedCoin(IEnumerable<CoinData> src, SelectedCoinData selectedCoinData)
        {
            var needExtraCost = selectedCoinData.GetCoinBody()
                                .Effects
                                .Where(effect => effect is Coin.Effect.IEffectNeedExtraCost)
                                .Cast<Coin.Effect.IEffectNeedExtraCost>()
                                .Sum(nec => nec.Value);
            if (needExtraCost == 0) return new List<CoinData>();

            // 使おうとしているコインが含まれるなら、それ以外から選択
            var temp = src.ToList();
            temp.RemoveAll(cd => cd.ID == selectedCoinData.CoinData.ID);

            return QueryNeedCoin(temp, needExtraCost);
        }

        static List<CoinData> QueryNeedCoin(List<CoinData> src, int needExtraCost)
        {
            if (needExtraCost < src.Sum(cd => cd.GetCoin().Size)) return null;
            if (needExtraCost <= 0) return new List<CoinData>();

            var item = QueryMostUselessCoin(src, needExtraCost);
            if (item == null) return null;

            src.Remove(item);
            needExtraCost -= item.GetCoin().Size;
            
            var results = QueryNeedCoin(src, needExtraCost); // 再帰
            if (results == null) return null;
            results.Add(item);
            return results;
        }

        static bool IsKeepPut(Coin.Body.Core coinBody)
        {
            switch(coinBody)
            {
                case Coin.Body.Set.Environment.IEnvironment:
                case Coin.v1.Body.BattleDoll1th:
                case Coin.v1.Body.HeresyIdol:
                case Coin.v1.Body.NoTrespassing:
                case Coin.v1.Body.RevolutionByCrowd:
                case Coin.v1.Body.Scapegoat:
                case Coin.v1.Body.SetTurnToEvolution:
                case Coin.v1.Body.WinFlag:
                    return true;
                default: return false;
            }
        }

        private ActionItem SelectSetAttack(Player player, IEnumerable<SelectedCoinData> uses)
        {
            WriteLog(nameof(SelectSetAttack));

            var currentArea = duelData.FieldData.AreaDatas[player.CurrentAreaNo];

            // 設置を維持すべき設置コインがある場合は、重ね置き可能なものだけ
            if (currentArea.GetCoinsByOwner(player).Any(scd => IsKeepPut(scd.GetCoinBody())))
            {
                uses = uses.Where(use => use.IsCoexistence());
            }

            // 現在のエリアのダメージ
            int currentAreaTotalDamage = currentArea.GetCoinsByOwner(player.PlayerNo).Sum(item => item.GetSetAttackDamage(duelData));

            // 敵対プレイヤー全員のライフ全てを奪えるダメージ量が既にあるなら、置き換えはしない
            if (duelData.GetOtherTeamPlayers(player.PlayerNo).All(player => player.Life <= currentAreaTotalDamage)) return null;

            // 今よりダメージが多くなる場合に配置
            var coins = uses.Select(coin =>
                            {
                                int damage = coin.GetSetAttackDamage(duelData, true);
                                if (coin.IsCoexistence())
                                {
                                    damage += currentAreaTotalDamage;
                                }
                                return new { Coin = coin, Damage = damage };
                            })
                            .Where(item => currentAreaTotalDamage < item.Damage)
                            .OrderByDescending(item => item.Damage)
                            .Select(item => item.Coin);

            return GetUseCoinInfo(player, coins);
        }

        public ActionItem SelectCoin(Player player, Defines.CoinType selectCoinType)
        {
            var usableCoins = player.Hand
                                .GetSeparateItems()
                                .Where(scd => scd.GetCoinBody().IsCoinType(selectCoinType))
                                .Where(scd => scd.IsUsable(duelData))
                                .ToList();
            if (!duelData.IsMustUse())
            {
                var mustUseCoins = usableCoins.Where(scd => scd.IsUseForce()).ToList();
                if (mustUseCoins.Any())
                {
                    usableCoins = mustUseCoins;
                }
            }

            if (selectCoinType.HasFlag(Defines.CoinType.SetAttack))
            {
                var traps = usableCoins.Where(scc => scc.GetCoinBody().IsCoinType(Defines.CoinType.SetAttack)).ToList();
                if (0 < traps.Count)
                {
                    var actionItem = SelectSetAttack(player, traps);
                    if (actionItem != null) return actionItem;
                }
            }
            
            if (selectCoinType.HasFlag(Defines.CoinType.Set))
            {
                var scdSets = usableCoins.Where(scd => scd.GetCoinBody().IsCoinType(Defines.CoinType.Set));

                // コインが設置済みである場合、重ね置き可能なものだけ対象
                if (duelData.FieldData.AreaDatas[player.CurrentAreaNo].GetCoinsByOwner(player).Any())
                {
                    scdSets = scdSets.Where(scd => scd.IsCoexistence());
                }

                var actionItem = GetUseCoinInfo(player, scdSets);
                if (actionItem != null) return actionItem;
            }
            
            if (selectCoinType.HasFlag(Defines.CoinType.DirectAttack))
            {
                var directAttaks = usableCoins
                                    .Where(scc => scc.GetCoinBody().IsCoinType(Defines.CoinType.DirectAttack))
                                    .OrderByDescending(item => item.GetDirectAttackDamage(duelData));
                var actionItem = GetUseCoinInfo(player, directAttaks);
                if (actionItem != null) return actionItem;
            }

            if (selectCoinType.HasFlag(Defines.CoinType.Support))
            {
                var supports = usableCoins.Where(scc => scc.GetCoinBody().IsCoinType(Defines.CoinType.Support)).ToList();
                if (0 < supports.Count)
                {
                    var actionItem = SelectSupport(player.Hand.Items, supports);
                    if (actionItem != null) return actionItem;
                }
            }
            return null;
        }

        public int ThrowDice(int playerNo)
        {
            return duelData.QueryDice(playerNo);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        static void WriteLog(params object[] messages)
        {
            WriteLog(LogType.Log, string.Join(" ", messages));
        }

        [System.Diagnostics.Conditional("DEBUG")]
        static void WriteLog(LogType logType, params object[] messages)
        {
            Functions.WriteLog(logType, nameof(AutoPlayerInputer), string.Join(",", messages));
        }

        public bool IsExecutableAdditionalDraw()
        {
            var duelRule = duelData.GameRule.DuelRule;
            if (!duelRule.CanAdditionalDraw()) return false;

            var player = duelData.GetCurrentTurnPlayer();

            int actionNum = 2;
            int drawUseResource = duelRule.GetDrawUseResource() + duelRule.GetAdditionalDrawUseResource();

            // リソースが余っていて、かつ手持ちに空きがあるなら追加で引く
            return drawUseResource * actionNum <= player.TurnResource &&
                (!duelData.HasHandCoinSizeLimit() || player.Hand.GetTotalSize() + 3 < duelData.GetMaxHandCoinSize()) &&
                (!duelData.HasHandCoinNumLimit() || player.Hand.GetCount() < actionNum);
        }
    }
}
