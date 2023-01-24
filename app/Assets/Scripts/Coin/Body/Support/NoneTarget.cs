using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using Assets.Scripts.Duel;

namespace Assets.Scripts.Coin.Body.Support
{
    abstract class NoneTarget : Core
    {
        public NoneTarget(string coinName)
            : base(coinName)
        {

        }

        public override UniTask<ActionItem> Select(DuelData duelData, ManualPlayManager manualPlayManager, ActionItem actionItem)
        {
            actionItem.SupportAction = new Duel.SupportAction.SupportActionNoneTarget();
            return UniTask.FromResult(actionItem);
        }

        public abstract bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce);
    }
}
