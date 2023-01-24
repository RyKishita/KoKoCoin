using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class ClearCoinStatusSetCoin : Scripts.Coin.Body.Support.TargetSetCoin
    {
        public ClearCoinStatusSetCoin(string coinName)
            : base(coinName)
        {
        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string status = Defines.GetLocalizedString(Defines.StringEnum.CoinStatus);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(status), status },
                    };

                    yield return GetLocalizedString(nameof(ClearCoinStatusSetCoin), param);
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
            return duelData.FieldData.GetCoinByHasStatus().Any();
        }

        public override bool IsMatchTarget(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData targetSelectedCoinData)
        {
            return !targetSelectedCoinData.CoinData.StatusList.IsEmpty();
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

            target.CoinData.StatusList.Clear();
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

            // ダメージ系効果を確認
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
            }
            {
                // 自分以外のコインのうち、プラスのダメージ補正があるコインを対象にする
                var coins = targetAreaNo.HasValue
                    ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                    : duelData.FieldData.GetAllAreaCoins().ToList();

                var result = coins
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
            // ダメージ以外の効果は付与した人が他人なら対象
            {
                var coins = targetAreaNo.HasValue
                    ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                    : duelData.FieldData.GetAllAreaCoinsOrderCenter().ToList();

                var result = coins
                    .Where(scd =>
                    {
                        if (scd.CoinData.StatusList.IsEmpty()) return false;
                        if (scd.CoinData.StatusList.Items.All(status => status.RegisteredPlayerNo == playerNo)) return false;
                        return true;
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

            if (bUseForce)
            {
                var coins = targetAreaNo.HasValue
                    ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                    : duelData.FieldData.GetAllAreaCoinsOrderOutSide().ToList();

                var result = coins
                    .Where(scd => !scd.CoinData.StatusList.IsEmpty())
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
                    nameof(ClearCoinStatusSetCoin),
                    actionItem.SelectedCoinData.CoinData.CoinName,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            actionItem.SupportAction = null;
            return false;
        }
    }
}
