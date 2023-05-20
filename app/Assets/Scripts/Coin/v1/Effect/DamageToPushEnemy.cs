using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class DamageToPushEnemy : DamageTo
    {
        public DamageToPushEnemy(int num)
        {
            moveNum = num;
        }

        public override string Explain
        {
            get
            {
                var param = new Dictionary<string, string>()
                {
                    { "value", moveNum.ToString() },
                };

                return GetEffectLocalizedString(nameof(DamageToPushEnemy), param);
            }
        }

        readonly int moveNum;

        public override UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterDamageCoin afterDamageCoin)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            var ownerPlayer = duelManager.DuelData.Players[playerNo];
            var damagedPlayerNo = afterDamageCoin.TakePlayerNo;
            var damagedPlayer = duelManager.DuelData.Players[damagedPlayerNo];

            bool bDirectMove;
            switch(selectedCoinData.GetCoinBody())
            {
                case Scripts.Coin.Body.Set.Core _:
                    bDirectMove = !damagedPlayer.IsLookDirect(); // 向いている方向の逆
                    break;
                case Scripts.Coin.Body.DirectAttack.Core _:
                case Scripts.Coin.Body.Guard.Core _:
                    {
                        int distance = (ownerPlayer.CurrentAreaNo - damagedPlayer.CurrentAreaNo);
                        bDirectMove = (distance == 0)
                                            ? ownerPlayer.IsLookDirect()
                                            : distance < 0;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }

            duelManager.RegistDuelEventAction(new ActionPush()
            {
                Target = selectedCoinData,
                PlayerNo = damagedPlayerNo,
                PushPower = moveNum * (bDirectMove ? 1 : -1)
            });

            return UniTask.CompletedTask;
        }
    }
}
