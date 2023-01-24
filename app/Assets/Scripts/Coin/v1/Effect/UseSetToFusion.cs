using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseSetToFusion : UseTo
    {
        public UseSetToFusion(int rate)
        {
            this.rate = rate;
        }

        public override string Explain
        {
            get
            {
                string trashcoin = Defines.GetLocalizedString(Defines.CoinPosition.Trash);

                var param = new Dictionary<string, string>()
                {
                    { nameof(trashcoin), trashcoin},
                    { nameof(rate), rate.ToString()},
                };

                return GetEffectLocalizedString(nameof(UseSetToFusion), param);
            }
        }

        readonly int rate;

        IEnumerable<SelectedCoinData> GetTargetAreaCoins(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (base.IsReceiveEventUse(duelData, selectedCoinData, after))
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                int currentAreaNo = duelData.Players[playerNo].CurrentAreaNo;

                var fieldData = duelData.FieldData;

                var targetAreaNos = new List<int>();
                targetAreaNos.Add(currentAreaNo);
                targetAreaNos.AddRange(fieldData.GetSideAreaNos(currentAreaNo));

                return targetAreaNos.SelectMany(areaNo =>
                            fieldData.AreaDatas[areaNo]
                                .GetCoinsByOwner(playerNo)
                                .Where(scc => scc.GetCoinBody() is Scripts.Coin.Body.SetAttack.Core));
            }
            return Enumerable.Empty<SelectedCoinData>();
        }

        protected override bool IsReceiveEventUse(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return GetTargetAreaCoins(duelData, selectedCoinData, after).Any();
        }

        protected override bool IsReceiveEventUseGuard(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return false; // 設置時効果なので防御時は常に一致しない
        }

        protected override UniTask ReceiveEventUseBodyAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, AfterEndUse afterUse)
        {
            var trapCoins = GetTargetAreaCoins(duelManager.DuelData, selectedCoinData, afterUse).ToList();

            if (trapCoins.Any())
            {
                var duelData = duelManager.DuelData;

                var coinIDs = new List<int>();

                foreach (var trapCoin in trapCoins)
                {
                    int damage = trapCoin.GetSetAttackDamage(duelData);

                    int value = damage * rate / 100;

                    // 持っていたダメージアップも取り込み
                    var status = trapCoin.CoinData.StatusList.GetItem<Duel.CoinStatus.CoinStatusAppendValue>();
                    if (status != null)
                    {
                        value += status.Value * rate / 100;
                    }

                    if (0 != value)
                    {
                        duelManager.RegistDuelEventAction(new ActionAddCoinStatus()
                        {
                            CoinID = trapCoin.CoinData.ID,
                            CoinStatus = new Duel.CoinStatus.CoinStatusAppendValue() { Value = value }
                        });

                        duelManager.RegistDuelEventAction(new ActionEffectSetCoin()
                        {
                            CoinIDs = new List<int>() { trapCoin.CoinData.ID },
                            ParticleType = 0 < value ? Defines.ParticleType.Buf : Defines.ParticleType.Debuf
                        });
                    }

                    coinIDs.Add(trapCoin.CoinData.ID);
                }

                duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
                {
                    CoinIDs = coinIDs,
                    DstCoinPosition = Defines.CoinPosition.Trash,
                    CoinMoveReason = Defines.CoinMoveReason.Effect
                });
            }

            return UniTask.CompletedTask;
        }
    }
}
