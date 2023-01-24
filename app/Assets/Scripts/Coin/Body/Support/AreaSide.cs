using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;
using Assets.Scripts.Duel;
using System.Collections.Generic;

namespace Assets.Scripts.Coin.Body.Support
{
    abstract class AreaSide : Core
    {
        public AreaSide(string coinName)
            : base(coinName)
        {

        }

        public override async UniTask<ActionItem> Select(DuelData duelData, ManualPlayManager manualPlayManager, ActionItem actionItem)
        {
            return await manualPlayManager.SelectSupportCoinAreaSide(actionItem)
                ? actionItem
                : null;
        }

        public abstract bool SelectAuto(DuelData duelData, ActionItem actionItem, ActionItem.TargetAreaStep targetAreaStep, bool bUseForce);
    }
}
