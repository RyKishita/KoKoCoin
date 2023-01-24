using Assets.Scripts.Duel;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Body.Support
{
    public abstract class Core : Body.Core
    {
        public Core(string coinName)
            : base(coinName)
        {

        }

        public override Defines.CoinType CoinType { get; } = Defines.CoinType.Support;

        public abstract UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem);

        public abstract UniTask<ActionItem> Select(DuelData duelData, ManualPlayManager manualPlayManager, ActionItem actionItem);
    }
}
