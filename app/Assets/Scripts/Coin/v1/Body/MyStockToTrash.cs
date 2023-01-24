using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class MyStockToTrash : Scripts.Coin.Body.Support.NoneTarget
    {
        public MyStockToTrash(string coinName, int stockToTrashValue)
            : base(coinName)
        {
            this.stockToTrashValue = stockToTrashValue;
        }

        readonly int stockToTrashValue;

        public Defines.CoinPosition NeedCoinPosition => Defines.CoinPosition.Stock;

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    var src = Defines.GetLocalizedString(NeedCoinPosition);
                    var dst = Defines.GetLocalizedString(Defines.CoinPosition.Trash);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(src), src},
                        { nameof(dst), dst},
                        { "value", stockToTrashValue.ToString()},
                    };

                    yield return GetLocalizedString(nameof(MyStockToTrash), param);
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

            var trashCoins = duelManager.DuelData.Players[targetPlayerNo].Stock.Items.Take(stockToTrashValue).Select(coin => coin.ID).ToList();
            if (trashCoins.Count == 0)
            {
                duelManager.WriteLog(UnityEngine.LogType.Warning, "ここに来る場合はロジックがおかしい");
                return UniTask.CompletedTask;
            }

            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinIDs = trashCoins,
                DstCoinPosition = Defines.CoinPosition.Trash,
                CoinMoveReason = Defines.CoinMoveReason.Effect
            });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            // 使いたいから入れているはずなのでチェックせず実行
            return true;
        }
    }
}
