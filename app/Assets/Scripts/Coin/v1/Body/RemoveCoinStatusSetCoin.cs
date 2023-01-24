using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class RemoveCoinStatusSetCoin : Scripts.Coin.Body.Support.TargetSetCoin
    {
        public RemoveCoinStatusSetCoin(string coinName, Duel.CoinStatus.ICoinStatus targetStatus)
            : base(coinName)
        {
            if (targetStatus == null) throw new ArgumentNullException(nameof(targetStatus));
            this.TargetStatus = targetStatus;
        }

        public Duel.CoinStatus.ICoinStatus TargetStatus { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string settedcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);
                    var abnormalstate = Defines.GetLocalizedString(Defines.StringEnum.AbnormalState);
                    string name = TargetStatus.ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(settedcoin), settedcoin},
                        { nameof(abnormalstate), abnormalstate },
                        { nameof(name), name},
                    };

                    yield return GetLocalizedString(nameof(RemoveCoinStatusSetCoin), param);
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
            return duelData.FieldData.GetCoinByHasStatus(TargetStatus).Any();
        }

        public override bool IsMatchTarget(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData targetSelectedCoinData)
        {
            return targetSelectedCoinData.CoinData.StatusList.Has(TargetStatus);
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var coinID = (actionItem.SupportAction as Duel.SupportAction.SupportActionTargetSetCoin).CoinID;
            if (!coinID.HasValue)
            {
                duelManager.WriteLog(UnityEngine.LogType.Warning, "ロジックがおかしい");
                return UniTask.CompletedTask;
            }

            var target = duelManager.DuelData.FieldData.GetCoin(coinID.Value);
            if (target == null)
            {
                duelManager.WriteLog(UnityEngine.LogType.Warning, "設置されていないコインが対象になった");
                return UniTask.CompletedTask;
            }
            target.CoinData.StatusList.RemoveBy(TargetStatus);
            duelManager.RegistDuelEventAction(new ActionEffectSetCoin()
            {
                CoinIDs = new List<int>() { coinID.Value },
                ParticleType = Defines.ParticleType.Cure
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

            switch (TargetStatus)
            {
                case Duel.CoinStatus.CoinStatusAppendValue _:
                    {
                        // 自分のコインのうち、マイナスのダメージ補正があるコインを対象にする
                        var coins = targetAreaNo.HasValue
                            ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                            : duelData.FieldData.GetAllAreaCoins().ToList();

                        var result = coins
                            .Where(scd => scd.CoinData.OwnerPlayerNo == playerNo)
                            .Select(scd => new
                            {
                                scd,
                                addValues = scd.CoinData.StatusList.GetItem<Duel.CoinStatus.CoinStatusAppendValue>()?.Value ?? 0
                            })
                            .Where(item => item.addValues < 0)
                            .OrderBy(item => item.addValues)
                            .Select(item => item.scd)
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

                        // 自分以外のコインのうち、プラスのダメージ補正があるコインを対象にする
                        result = coins
                            .Where(scd => scd.CoinData.OwnerPlayerNo != playerNo)
                            .Select(scd => new
                            {
                                scd,
                                addValues = scd.CoinData.StatusList.GetItem<Duel.CoinStatus.CoinStatusAppendValue>()?.Value ?? 0
                            })
                            .Where(item => 0 < item.addValues)
                            .OrderByDescending(item => item.addValues)
                            .Select(item => item.scd)
                            .FirstOrDefault();
                        if (result != null) break;
                    }
                    break;
                default:
                    {   // ダメージ以外の効果は付与した人が他人なら対象
                        var coins = targetAreaNo.HasValue
                            ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                            : duelData.FieldData.GetAllAreaCoinsOrderCenter().ToList();

                        var result = coins
                            .Where(scd =>
                            {
                                var status = scd.CoinData.StatusList.GetItem(TargetStatus.GetType());
                                if (status == null) return false;
                                return status.RegisteredPlayerNo != playerNo;
                            })
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
                    }
                    break;
            }
            if (bUseForce)
            {
                var coins = targetAreaNo.HasValue
                    ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                    : duelData.FieldData.GetAllAreaCoinsOrderOutSide().ToList();

                var result = coins
                    .Where(scd => scd.CoinData.StatusList.Has(TargetStatus))
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
                    nameof(RemoveCoinStatusSetCoin),
                    actionItem.SelectedCoinData.CoinData.CoinName,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            actionItem.SupportAction = null;
            return false;
        }
    }
}
