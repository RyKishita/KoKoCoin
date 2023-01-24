using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseGuardToPushEnemy : UseTo
    {
        public UseGuardToPushEnemy(int num)
        {
            this.num = num;
        }

        readonly int num;

        public override string Explain
        {
            get
            {
                int format = num < 0 ? 0 : 1;

                string player = Defines.GetLocalizedString(Defines.StringEnum.Opponent);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { "value", num.ToString() },
                };

                return GetEffectLocalizedString(nameof(UseGuardToPushEnemy), format, param);
            }
        }

        protected override bool IsReceiveEventUse(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return false;//自分を移動する事はなく、相手も特定できないので常に処理しない
        }

        protected override UniTask ReceiveEventUseGuardBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUseGuard afterUseGuard)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            var player = duelManager.DuelData.Players[playerNo];
            var otherPlayerNo = afterUseGuard.DirectAttack.CoinData.OwnerPlayerNo;
            var otherPlayer = duelManager.DuelData.Players[otherPlayerNo];

            int distance = (player.CurrentAreaNo - otherPlayer.CurrentAreaNo);
            bool bDirectMove = (distance == 0)
                                ? player.IsLookDirect()
                                : distance < 0;

            duelManager.RegistDuelEventAction(new ActionPush()
            {
                Target = selectedCoinData,
                PlayerNo = otherPlayerNo,
                PushPower = num * (bDirectMove ? 1 : -1)
            });

            return UniTask.CompletedTask;
        }
    }
}
