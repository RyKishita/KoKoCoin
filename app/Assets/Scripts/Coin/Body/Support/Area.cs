using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;
using Assets.Scripts.Duel;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Coin.Body.Support
{
    abstract class Area : Core
    {
        public Area(string coinName)
            : base(coinName)
        {

        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;
            return GetTargetAreaNos(duelData).Any();
        }

        public virtual List<int> GetTargetAreaNos(DuelData duelData)
        {
            return duelData.GetAreaNos().ToList();
        }

        public override async UniTask<ActionItem> Select(DuelData duelData, ManualPlayManager manualPlayManager, ActionItem actionItem)
        {
            var selectedAreaNo = await manualPlayManager.SelectSupportCoinArea(duelData.CurrentTurnPlayerNo, actionItem, GetTargetAreaNos(duelData));
            if (!selectedAreaNo.HasValue) return null;
            actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetArea() { AreaNo = selectedAreaNo.Value };
            return actionItem;
        }

        public abstract bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce);
    }
}
