using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class MyTrashToExclusion : Scripts.Coin.Body.Support.NoneTarget
    {
        public MyTrashToExclusion(string coinName)
            : base(coinName)
        {

        }

        public Defines.CoinPosition NeedCoinPosition => Defines.CoinPosition.Trash;

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    var src = Defines.GetLocalizedString(NeedCoinPosition);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(src), src},
                    };

                    yield return GetLocalizedString(nameof(MyTrashToExclusion), param);
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
            return !duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].GetCoinDataList(NeedCoinPosition).IsEmpty();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var targetPlayerNo = actionItem.GetPlayerNo();
            var targetPlayer = duelManager.DuelData.Players[targetPlayerNo];

            var trashCoins = targetPlayer.Trash.Items;
            if (trashCoins.Count == 0)
            {
                duelManager.WriteLog(UnityEngine.LogType.Warning, "ここに来る場合はロジックがおかしい");
                return UniTask.CompletedTask;
            }

            duelManager.RegistDuelEventAction(new ActionMoveCoinsToExclusion()
            {
                CoinIDs = trashCoins.Select(coin => coin.ID).ToList(),
                CoinMoveReason = Defines.CoinMoveReason.Effect
            });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            // 使いたいから入れているはずなのでチェックせず実行
            // 本来はテーマに沿った判断が必要
            return true;
        }
    }
}
