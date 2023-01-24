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
    class OwnerSetDamage : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public OwnerSetDamage(int turn)
        {
            if (turn < 0) throw new ArgumentOutOfRangeException(nameof(turn));
            this.turn = turn;
        }

        readonly int turn;

        public override string Explain
        {
            get
            {
                int formatType = (0 < turn) ? 0 : 1;

                string trashcoin = Defines.GetLocalizedString(Defines.CoinPosition.Trash);

                var param = new Dictionary<string, string>()
                {
                    { nameof(turn), turn.ToString()},
                    { nameof(trashcoin), trashcoin},
                };

                return GetEffectLocalizedString(nameof(OwnerSetDamage), formatType, param);
            }
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            int? areaNo = duelData.FieldData.GetContainCoinAreaNo(selectedCoinData);
            int currentturn = selectedCoinData.CoinData.Turn;
            return after is AfterMovePlayer afterMovePlayer &&
                    afterMovePlayer.PlayerNo == selectedCoinData.CoinData.OwnerPlayerNo &&
                    (turn <= 0 || 0 < currentturn && (currentturn % turn) == 0) &&
                    areaNo == afterMovePlayer.AreaNo;
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                duelManager.RegistDuelEventAction(new ActionDamageCoin()
                {
                    ReasonPlayerNo = -1,
                    DiffencePlayerNo = selectedCoinData.CoinData.OwnerPlayerNo,
                    DamageSource = selectedCoinData,
                    Damage = selectedCoinData.GetSetAttackDamage(duelManager.DuelData)
                });
                duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    CoinMoveReason = Defines.CoinMoveReason.Effect,
                    DstCoinPosition = Defines.CoinPosition.Trash,
                    IsForce = true
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
