using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;
using Cysharp.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    /// <summary>
    /// コインが設置エリアから離れた時、このコインを元のエリアに配置する
    /// </summary>
    class AutoSetByMoveCoinName : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public AutoSetByMoveCoinName(bool bCoinOwner, Defines.CoinPosition coinPosition, string coinName)
        {
            this.bCoinOwner = bCoinOwner;
            this.coinPosition = coinPosition;
            this.coinName = coinName;
        }

        readonly bool bCoinOwner;

        public override string Explain
        {
            get
            {
                int formatType;
                string coinname;
                if (string.IsNullOrEmpty(coinName))
                {
                    coinname = string.Empty;
                    formatType = 0;
                }
                else
                {
                    coinname = CoinList.Instance.GetCoin(coinName).DisplayName;
                    formatType = 1;
                }
                if (!bCoinOwner)
                {
                    formatType += 2;
                }

                string coinposition = Defines.GetLocalizedString(coinPosition);
                string setcoin = Defines.GetLocalizedString(Defines.CoinType.Set);
                string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);

                var param = new Dictionary<string, string>()
                {
                    { nameof(coinposition), coinposition },
                    { nameof(coinname), coinname },
                    { nameof(setcoin), setcoin},
                    { nameof(field), field},
                };

                return GetEffectLocalizedString(nameof(AutoSetByMoveCoinName), formatType, param);
            }
        }

        readonly Defines.CoinPosition coinPosition;
        readonly string coinName;

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (!duelData.IsMatchPosition(selectedCoinData, coinPosition)) return false;

            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

            if (after is AfterMoveCoinsToPlayer afterMoveCoinsToPlayer)
            {
                var coinIDs = afterMoveCoinsToPlayer.SrcItems
                                .Where(si => si.CoinPosition == Defines.CoinPosition.Field)
                                .Where(si =>
                                {
                                    var coinData = duelData.GetCoin(si.CoinID);
                                    return (bCoinOwner == (coinData.OwnerPlayerNo == playerNo)) &&
                                        (string.IsNullOrEmpty(coinName) || coinData.CoinName == coinName);

                                })
                                .Select(si => si.CoinID);

                //移動対象に自分が含まれていたら処理しない
                return !coinIDs.Contains(selectedCoinData.CoinData.ID);
            }

            return false;
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            if (IsReceiveEvent(duelManager.DuelData, selectedCoinData, after))
            {
                var afterMoveCoinsToPlayer = after as AfterMoveCoinsToPlayer;

                duelManager.RegistDuelEventAction(new ActionMoveCoinsToSet()
                {
                    CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                    DstAreaNo = afterMoveCoinsToPlayer.SrcItems.First().AreaNo.Value,//異なるエリアにある設置コインが同時に移動した場合は、始めのコインを参照
                    CoinMoveReason = Defines.CoinMoveReason.Effect,
                    IsForce = true
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
