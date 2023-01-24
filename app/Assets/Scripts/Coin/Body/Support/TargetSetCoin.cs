using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.Body.Support
{
    abstract class TargetSetCoin : Core
    {
        public TargetSetCoin(string coinName)
            : base(coinName)
        {

        }

        public abstract bool IsMatchTarget(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData targetSelectedCoinData);

        public override async UniTask<ActionItem> Select(DuelData duelData, ManualPlayManager manualPlayManager, ActionItem actionItem)
        {
            return await manualPlayManager.SelectSupportCoinSetCoinAsync(actionItem, scd => IsMatchTarget(duelData, actionItem.SelectedCoinData, scd))
                ? actionItem
                : null;
        }

        public abstract bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce);
    }
}
