using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class ReturnTrashToHand : Scripts.Coin.Body.Support.CoinOwnerCoins
    {
        public ReturnTrashToHand(string coinName)
            : base(coinName, Defines.CoinPosition.Trash)
        {

        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var target = (actionItem.SupportAction as Duel.SupportAction.SupportActionTargetCoin);

            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinMoveReason = Defines.CoinMoveReason.Effect,
                CoinIDs = new List<int>() { target.CoinID },
                DstCoinPosition = Defines.CoinPosition.Hand
            });
            return UniTask.CompletedTask;
        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;
            return !duelData
                    .Players[selectedCoinData.CoinData.OwnerPlayerNo]
                    .GetCoinDataList(CoinPosition)
                    .IsEmpty();
        }


        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string src = Defines.GetLocalizedString(CoinPosition);
                    string dst = Defines.GetLocalizedString(Defines.CoinPosition.Hand);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(src), src },
                        { nameof(dst), dst },
                    };

                    yield return GetLocalizedString(nameof(ReturnTrashToHand), param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, Defines.CoinPosition? targetCoinPosition, bool bUseForce)
        {
            if (targetCoinPosition != CoinPosition) return false;

            var playerNo = actionItem.GetPlayerNo();

            // 一番大きいコインを取る。本来はデッキのテーマで異なる
            var coin = duelData.Players[playerNo]
                        .GetCoinDataList(CoinPosition)
                        .Items
                        .OrderByDescending(cd => cd.GetCoin().Size)
                        .FirstOrDefault();
            if (coin == null)
            {
                if (bUseForce)
                {
                    Utility.Functions.WriteLog(UnityEngine.LogType.Warning,
                        nameof(ReturnTrashToHand),
                        actionItem.SelectedCoinData.CoinData.CoinName,
                        "使用できる時点でいずれかは対象になるはず"
                    );
                }
                return false;
            }

            actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetCoin()
            {
                PlayerNo = playerNo,
                CoinID = coin.ID
            };
            return true;
        }
    }
}
