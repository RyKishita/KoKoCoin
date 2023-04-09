using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class SetToSideCopy : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectTriggerEvent
    {
        public SetToSideCopy(string copiedCoinName)
        {
            this.copiedCoinName = copiedCoinName;
        }

        readonly string copiedCoinName;

        public override string Explain
        {
            get
            {
                string setcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);
                string coinname = CoinList.Instance.GetCoin(copiedCoinName).DisplayName;

                var param = new Dictionary<string, string>()
                {
                    { nameof(setcoin), setcoin},
                    { nameof(coinname), coinname},
                };

                return GetEffectLocalizedString(nameof(SetToSideCopy), param);
            }
        }

        public override IEnumerable<string> GetCopiedCoinNames()
        {
            return base.GetCopiedCoinNames().Concat(new[] { copiedCoinName });
        }

        IEnumerable<int> GetEmptySideAreaNos(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            if (after is AfterMoveCoinsToSet afterMoveCoinsToSet &&
                afterMoveCoinsToSet.CoinMoveReason == Defines.CoinMoveReason.Set &&
                afterMoveCoinsToSet.SrcItems.Any(item => item.CoinID == selectedCoinData.CoinData.ID))
            {
                var fieldData = duelData.FieldData;
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

                return fieldData.GetSideAreaNosNotSafe(afterMoveCoinsToSet.DstAreaNo).Where(areaNo => !fieldData.AreaDatas[areaNo].GetCoinsByOwner(playerNo).Any());
            }
            return Enumerable.Empty<int>();
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After after)
        {
            return GetEmptySideAreaNos(duelData, selectedCoinData, after).Any();
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After after)
        {
            var sideAreaNos = GetEmptySideAreaNos(duelManager.DuelData, selectedCoinData, after);

            if (sideAreaNos.Any())
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;

                foreach (int sideAreaNo in sideAreaNos)
                {
                    var copySetCoin = duelManager.DuelData
                                    .CreateCopy(selectedCoinData.CoinData.ID, copiedCoinName, playerNo)
                                    .MakeSetCoin();

                    duelManager.RegistDuelEventAction(new ActionAddCopyToSet()
                    {
                        Target = copySetCoin,
                        AreaNo = sideAreaNo
                    });
                }
            }

            return UniTask.CompletedTask;
        }

        public override bool IsOnAreaEffect()
        {
            return false;
        }
    }
}
