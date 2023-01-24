using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class MyCoinToStock : Scripts.Coin.Body.Support.NoneTarget
    {
        public MyCoinToStock(string coinName, Defines.CoinPosition coinPosition)
            : base(coinName)
        {
            if (coinPosition == Defines.CoinPosition.Stock) throw new ArgumentException("指定できない値", nameof(coinPosition));
            SrcCoinPosition = coinPosition;
        }

        public Defines.CoinPosition SrcCoinPosition { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    var src = Defines.GetLocalizedString(SrcCoinPosition);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(src), src},
                    };

                    yield return GetLocalizedString(nameof(MyCoinToStock), param);
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
            return !duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo].GetCoinDataList(SrcCoinPosition).IsEmpty();
        }

        public override async UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var playerNo = actionItem.GetPlayerNo();

            var currentTurnPlayerManager = duelManager.PlayerManagers[playerNo];
            duelManager.SetFocusies(
                new[] {
                    currentTurnPlayerManager.GetGameObject(Defines.CoinPosition.Stock),
                    currentTurnPlayerManager.GetGameObject(SrcCoinPosition)
                });
            var coinIDs = duelManager.DuelData
                .Players[playerNo]
                .GetCoinDataList(SrcCoinPosition)
                .Items
                .Select(item => item.ID)
                .ToList();
            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinIDs = coinIDs,
                DstCoinPosition = Defines.CoinPosition.Stock,
                CoinMoveReason = Defines.CoinMoveReason.Effect
            });
            await duelManager.ProcessDuelEvents();
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
