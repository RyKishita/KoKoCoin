using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class GuardToPushEnemy : GuardTo
    {
        public GuardToPushEnemy(int num)
        {
            PushEnemyNum = num;
        }

        public override string Explain
        {
            get
            {
                int format = 0 < PushEnemyNum ? 0 : 1;
                string player = Defines.GetLocalizedString(Defines.StringEnum.Attacker);

                var param = new Dictionary<string, string>()
                {
                    { nameof(player), player },
                    { "value", PushEnemyNum.ToString() },
                };

                return GetEffectLocalizedString(nameof(GuardToPushEnemy), format, param);
            }
        }

        public int PushEnemyNum { get; }

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterGuardNoDamage afterGuardNoDamage)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            var player = duelManager.DuelData.Players[playerNo];
            var otherPlayerNo = afterGuardNoDamage.DirectAttack.CoinData.OwnerPlayerNo;
            var otherPlayer = duelManager.DuelData.Players[otherPlayerNo];

            int distance = (player.CurrentAreaNo - otherPlayer.CurrentAreaNo);
            bool bDirectMove = (distance == 0)
                                ? player.IsLookDirect()
                                : distance < 0;

            duelManager.RegistDuelEventAction(new ActionPush()
            {
                Target = selectedCoinData,
                PlayerNo = otherPlayerNo,
                PushPower = PushEnemyNum * (bDirectMove ? 1 : -1)
            });

            return UniTask.CompletedTask;
        }
    }
}
