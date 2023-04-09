using Assets.Scripts.Duel;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class InvalidEffectSetCoin : Scripts.Coin.Body.Support.TargetSetCoin
    {
        public InvalidEffectSetCoin(string coinName)
            : base(coinName)
        {

        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    var param = new Dictionary<string, string>()
                    {
                        { "value", status.Turn.ToString() },
                    };

                    yield return GetLocalizedString(nameof(InvalidEffectSetCoin), param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        readonly Duel.CoinStatus.CoinStatusInvalidEffectByTurn status = new Duel.CoinStatus.CoinStatusInvalidEffectByTurn() { Turn = 3 };

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

            duelManager.RegistDuelEventAction(new Duel.DuelEvent.ActionAddCoinStatus()
            {
                CoinID = coinID.Value,
                CoinStatus = status
            });

            duelManager.RegistDuelEventAction(new Duel.DuelEvent.ActionEffectSetCoin()
            {
                CoinIDs = new List<int>() { coinID.Value },
                ParticleType = Defines.ParticleType.InvalidEffectSet
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

            //自分のコインなら、不利な効果打消し
            //他人のコインなら有利な効果打消し。TODO 踏んだら負ける設置攻撃コインのダメージ無効化にも使えるが対応していない
            var coins = targetAreaNo.HasValue
                ? duelData.FieldData.AreaDatas[targetAreaNo.Value].Coins
                : duelData.FieldData.GetAllAreaCoinsOrderCenter().ToList();

            var result = coins
                .Where(scd => scd.GetCoinBody().Effects.Any())
                .Where(scd =>
                    scd.CoinData.OwnerPlayerNo == playerNo
                        ? scd.GetCoinBody().Effects.All(effect => !effect.IsOnAreaEffect() && !effect.IsAdvantage(duelData))
                        : scd.GetCoinBody().Effects.Any(effect => effect.IsAdvantage(duelData)))
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
                result = duelData.FieldData.GetAllAreaCoinsOrderOutSide().FirstOrDefault();
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
                    nameof(InvalidEffectSetCoin),
                    actionItem.SelectedCoinData.CoinData.CoinName,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            actionItem.SupportAction = null;
            return false;            
        }
    }
}
