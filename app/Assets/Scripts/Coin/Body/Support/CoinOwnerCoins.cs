using Assets.Scripts.Duel;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Body.Support
{
    abstract class CoinOwnerCoins : Core
    {
        public CoinOwnerCoins(string coinName, Defines.CoinPosition coinPosition)
            : base(coinName)
        {
            CoinPosition = coinPosition;
        }

        public Defines.CoinPosition CoinPosition { get; }

        public override async UniTask<ActionItem> Select(DuelData duelData, ManualPlayManager manualPlayManager, ActionItem actionItem)
        {
            var result = await manualPlayManager.ProcessSupportSelectCoinAsync(actionItem, CoinPosition);
            if (result == null) return null;

            var playerNo = actionItem.GetPlayerNo();

            actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetCoin()
            {
                PlayerNo = playerNo,
                CoinID = result.Value
            };
            return actionItem;
        }

        public abstract bool SelectAuto(DuelData duelData, ActionItem actionItem, Defines.CoinPosition? targetCoinPosition, bool bUseForce);
    }
}
