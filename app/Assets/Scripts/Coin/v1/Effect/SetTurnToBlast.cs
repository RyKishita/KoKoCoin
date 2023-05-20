using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class SetTurnToBlast : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public SetTurnToBlast(int turn, int range, int damage)
        {
            this.turn = turn;
            this.range = range;
            this.damage = damage;
        }

        public override string Explain
        {
            get
            {
                string trash = Defines.GetLocalizedString(Defines.CoinPosition.Trash);

                var param = new Dictionary<string, string>()
                {
                    { nameof(trash), trash },
                    { nameof(turn), turn.ToString()},
                    { nameof(range), range.ToString()},
                    { nameof(damage), damage.ToString()},
                };

                return GetEffectLocalizedString(nameof(SetTurnToBlast), param);
            }
        }

        readonly int turn;
        readonly int range;
        readonly int damage;

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, Defines.CoinPosition.Field)) return false;

            if (after is AfterAddTurn afterAddTurn &&
                afterAddTurn.TargetCoinDataIDs.Contains(selectedCoinData.CoinData.ID) &&
                turn <= selectedCoinData.CoinData.Turn)
            {
                var owner = selectedCoinData.CoinData.OwnerPlayerNo;
                return duelData.GetOtherTeamPlayers(owner).Any(other=> IsTargetPlayer(duelData, selectedCoinData, other));
            }
            return false;
        }

        bool IsTargetPlayer(DuelData duelData, SelectedCoinData selectedCoinData, Player player)
        {
            if (duelData.FieldData.AreaDatas[player.CurrentAreaNo].GetAreaType() != Defines.AreaType.Safe) return false;

            var areaNo = duelData.FieldData.GetContainCoinAreaNo(selectedCoinData);
            if (!areaNo.HasValue) return false;

            int distance = System.Math.Abs(player.CurrentAreaNo - areaNo.Value);
            return distance <= range;
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                var duelData = duelManager.DuelData;
                duelManager.RegistDuelEventActions(
                    duelData.GetOtherTeamPlayers(selectedCoinData.CoinData.OwnerPlayerNo)
                    .Where(other => IsTargetPlayer(duelData, selectedCoinData, other))
                    .Select(other => new ActionDamageCoin()
                    {
                        ReasonPlayerNo = selectedCoinData.CoinData.OwnerPlayerNo,
                        TakePlayerNo = other.PlayerNo,
                        DamageSource = selectedCoinData,
                        Damage = damage
                    }));

                duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    DstCoinPosition = Defines.CoinPosition.Trash,
                    CoinMoveReason = Defines.CoinMoveReason.Effect
                });
            }

            return UniTask.CompletedTask;
        }

        public override bool? IsAdvantageProgressedTurn(DuelData duelData)
        {
            return true;
        }
    }
}
