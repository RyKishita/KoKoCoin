using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.Body.Set
{
    public class Core : Body.Core
    {
        public Core(string coinName)
            : base(coinName)
        {

        }

        public override Defines.CoinType CoinType { get; } = Defines.CoinType.Set;

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;

            var player = duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo];

            var areaType = duelData.FieldData.AreaDatas[player.CurrentAreaNo].GetAreaType();
            if (areaType == Defines.AreaType.Safe) return false;

            return true;
        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                if (IsCoexistence)
                {
                    yield return Defines.GetLocalizedString(Defines.StringEnum.Coexistence);
                }

                if (IsNoReverseSet)
                {
                    yield return Defines.GetLocalizedString(Defines.StringEnum.NoReverseSet);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                // コインが裏向きならまず表向きにする
                if (selectedCoinData.IsReverse)
                {
                    duelManager.RegistDuelEventAction(new ActionChangeReverse()
                    {
                        TargetCoin = selectedCoinData,
                        IsReverseDst = false,
                        IsForce = true
                    });
                }
            }

            await base.ReceiveEventAsync(duelManager, selectedCoinData, after);

            // 敵対プレイヤーが、このコインを設置したマスに止まった時
            if (IsOtherPlayerStop(duelManager, selectedCoinData, after))
            {
                // 捨てコインにする
                duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    DstCoinPosition = Defines.CoinPosition.Trash,
                    CoinMoveReason = Defines.CoinMoveReason.EnemyStopSet
                });
            }
        }

        protected bool IsOtherPlayerStop(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            return after is AfterMovePlayer afterMovePlayer &&
                duelManager.DuelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).Contains(afterMovePlayer.PlayerNo) &&
                duelManager.DuelData.FieldData.GetContainCoinAreaNo(selectedCoinData) == afterMovePlayer.AreaNo;
        }

        public virtual bool IsCoexistence => false;

        public virtual bool IsNoReverseSet => false;
    }
}
