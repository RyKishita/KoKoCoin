using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class RemoveCoinStatusALLSetCoins : Scripts.Coin.Body.Support.NoneTarget
    {
        public RemoveCoinStatusALLSetCoins(string coinName, Duel.CoinStatus.ICoinStatus targetStatus)
            : base(coinName)
        {
            if (targetStatus == null) throw new ArgumentNullException(nameof(targetStatus));
            TargetCoinStatus = targetStatus;
        }

        public Duel.CoinStatus.ICoinStatus TargetCoinStatus { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string status = Defines.GetLocalizedString(Defines.StringEnum.CoinStatus);
                    string settedcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);
                    string name = TargetCoinStatus.ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(settedcoin), settedcoin },
                        { nameof(status), status},
                        { nameof(name), name},
                    };

                    yield return GetLocalizedString(nameof(RemoveCoinStatusALLSetCoins), param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;
            return duelData.FieldData.GetCoinByHasStatus(TargetCoinStatus).Any();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var coinIDs = duelManager.DuelData
                        .FieldData
                        .GetAllAreaCoins()
                        .Where(scd => scd.CoinData.StatusList.RemoveBy(TargetCoinStatus))
                        .Select(scd => scd.CoinData.ID)
                        .ToList();
            duelManager.RegistDuelEventAction(new ActionEffectSetCoin()
            {
                CoinIDs = coinIDs,
                ParticleType = Defines.ParticleType.Cure
            });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            var playerNo = actionItem.GetPlayerNo();

            // 全てのステータスが自分が付与したものではないなら実行
            // 細かい事を言うと、自分も付与予定だがまだ実行していない場合は残したい
            if (duelData.FieldData
                .GetAllAreaCoins()
                .SelectMany(scd => scd.CoinData.StatusList.Items)
                .Where(status => status.IsMatch(TargetCoinStatus))
                .All(status => status.RegisteredPlayerNo != playerNo))
            {
                return true;
            }

            return false;
        }
    }
}
