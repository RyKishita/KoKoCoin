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
    /// <summary>
    /// 指定されたコインが移動する時、代わりに移動する
    /// 複数のコインを移動する時は、一つでも一致したら全てを置き換える
    /// </summary>
    class SubstitutionMoveCoinName : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectInterceptDuelAction
    {
        public SubstitutionMoveCoinName(string coinName)
        {
            this.coinName = coinName;
        }

        public override string Explain
        {
            get
            {
                int formatType;
                if (string.IsNullOrEmpty(coinName))
                {
                    formatType = 0;
                }
                else
                {
                    formatType = 1;
                }

                string coinname = string.IsNullOrWhiteSpace(coinName)? string.Empty : CoinList.Instance.GetCoin(coinName).DisplayName;
                string srccoinposition = Defines.GetLocalizedString(srcCoinPosition);
                string dstcoinposition = Defines.GetLocalizedString(dstCoinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinname), coinname },
                    { nameof(srccoinposition), srccoinposition },
                    { nameof(dstcoinposition), dstcoinposition },
                };

                return GetEffectLocalizedString(nameof(SubstitutionMoveCoinName), formatType, param);
            }
        }

        readonly string coinName;
        readonly Defines.CoinPosition coinPosition = Defines.CoinPosition.Field;
        readonly Defines.CoinPosition srcCoinPosition = Defines.CoinPosition.Field;
        readonly Defines.CoinPosition dstCoinPosition = Defines.CoinPosition.Trash;

        IEnumerable<int> GetTargetCoinIDs(DuelData duelData, SelectedCoinData selectedCoinData, Duel.DuelEvent.Action duelEventAction)
        {
            var ownerPlayerNo = selectedCoinData.CoinData.OwnerPlayerNo;

            var coinIDs = new List<int>();
            if (dstCoinPosition == Defines.CoinPosition.Field)
            {
                if (duelEventAction is ActionMoveCoinsToSet actionMoveCoinsToSet)
                {
                    coinIDs = actionMoveCoinsToSet.CoinIDs.Where(coinID => duelData.GetCoin(coinID).OwnerPlayerNo== ownerPlayerNo).ToList();
                }
            }
            else
            {
                if (duelEventAction is ActionMoveCoinsToPlayer actionMoveCoinsToPlayer &&
                    actionMoveCoinsToPlayer.DstCoinPosition == dstCoinPosition &&
                    actionMoveCoinsToPlayer.CoinMoveReason == Defines.CoinMoveReason.EnemyStopSet)
                {
                    coinIDs = actionMoveCoinsToPlayer.CoinIDs.Where(coinID => duelData.GetCoin(coinID).OwnerPlayerNo == ownerPlayerNo).ToList();
                }
            }

            coinIDs = coinIDs.Where(coinID =>
                                    (srcCoinPosition == Defines.CoinPosition.None || duelData.GetCoinPosition(coinID) == srcCoinPosition) &&
                                    (string.IsNullOrEmpty(coinName) || duelData.GetCoin(coinID).CoinName == coinName))
                .ToList();

            //移動対象に自分が含まれていたら処理しない
            if (coinIDs.Contains(selectedCoinData.CoinData.ID))
            {
                return Enumerable.Empty<int>();
            }

            return coinIDs;
        }

        public bool InterceptDuelAction(DuelManager duelManager, SelectedCoinData selectedCoinData, Duel.DuelEvent.Action duelEventAction)
        {
            if (!duelManager.DuelData.IsMatchPosition(selectedCoinData, coinPosition)) return false;

            if (!GetTargetCoinIDs(duelManager.DuelData, selectedCoinData, duelEventAction).Any()) return false;

            if (dstCoinPosition == Defines.CoinPosition.Field)
            {
                var actionMoveCoinsToSet = duelEventAction as ActionMoveCoinsToSet;

                duelManager.RegistDuelEventAction(new ActionMoveCoinsToSet()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    DstAreaNo = actionMoveCoinsToSet.DstAreaNo,
                    CoinMoveReason = Defines.CoinMoveReason.Effect,
                    IsForce = true
                });
            }
            else
            {
                duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    DstCoinPosition = dstCoinPosition,
                    CoinMoveReason = Defines.CoinMoveReason.Effect,
                    IsForce = true
                });
            }

            return true;
        }
    }
}
