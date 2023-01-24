using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class Redraw : Scripts.Coin.Body.Support.NoneTarget
    {
        public Redraw(string coinName)
            : base(coinName)
        {

        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                yield return GetLocalizedString(nameof(Redraw));

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;
            var player = duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo];
            if (player.Stock.IsEmpty()) return false;
            var handWork = player.Hand.Duplicate();
            handWork.UnRegist(selectedCoinData.CoinData);
            return !handWork.IsEmpty();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var coinData = actionItem.SelectedCoinData.CoinData;
            var ownerPlayerNo = coinData.OwnerPlayerNo;

            duelManager.RegistDuelEventAction(new ActionEffectPlayer() { PlayerNo = ownerPlayerNo, ParticleType = Defines.ParticleType.CoinDraw });

            var ownerPlayer = duelManager.DuelData.Players[ownerPlayerNo];

            var stockWork = ownerPlayer.Stock.Duplicate();
            var handWork = ownerPlayer.Hand.Duplicate();
            handWork.UnRegist(coinData);

            // 使用したコイン自体は含まないので、登録を外してからサイズ取得
            int currentSize = handWork.GetTotalSize();
            int currentNum = handWork.GetCount() - 1;

            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinIDs = handWork.Items.Select(item => item.ID).ToList(),
                DstCoinPosition = Defines.CoinPosition.Stock,
                CoinMoveReason = Defines.CoinMoveReason.Effect
            });

            handWork.Clear();

            var coinIDs = new List<int>();
            while (!stockWork.IsEmpty() &&
                    handWork.GetTotalSize() < currentSize &&
                    handWork.GetCount() < currentNum)
            {
                var cc = stockWork.Items.First();
                stockWork.UnRegist(cc);
                handWork.Regist(cc);

                coinIDs.Add(cc.ID);
            }

            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinIDs = coinIDs,
                DstCoinPosition = Defines.CoinPosition.Hand,
                CoinMoveReason = Defines.CoinMoveReason.Effect
            });

            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            // 何を引きたいかによるが、テーマによって異なるので考慮しない
            return true;
        }
    }
}
