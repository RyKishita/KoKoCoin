using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class ClearCoinStatusALLSetCoinsByPlayer : Scripts.Coin.Body.Support.NoneTarget
    {
        public ClearCoinStatusALLSetCoinsByPlayer(string coinName, bool bCoinOwner)
            : base(coinName)
        {
            IsTargetCoinOwner = bCoinOwner;
        }

        public bool IsTargetCoinOwner { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    int formatType = IsTargetCoinOwner ? 0 : 1;
                    string settedcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoin);
                    var status = Defines.GetLocalizedString(Defines.StringEnum.CoinStatus);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(settedcoin), settedcoin},
                        { nameof(status), status },
                    };

                    yield return GetLocalizedString(nameof(ClearCoinStatusALLSetCoinsByPlayer), formatType, param);
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
            var owner = selectedCoinData.CoinData.OwnerPlayerNo;
            return duelData.FieldData
                .GetCoinByHasStatus()
                .Where(scd => (scd.CoinData.OwnerPlayerNo == owner) == IsTargetCoinOwner)
                .Any();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var playerNo = actionItem.GetPlayerNo();

            var coinIDs = duelManager.DuelData.
                            FieldData
                            .GetAllAreaCoins()
                            .Where(scd => (scd.CoinData.OwnerPlayerNo == playerNo) == IsTargetCoinOwner)
                            .Where(scd => scd.CoinData.StatusList.Clear())
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

            if (IsTargetCoinOwner)
            {
                // 全てのステータスが自分が付与したものではないなら実行
                // 細かい事を言うと、自分も付与予定だがまだ実行していない場合は残したい
                if (duelData.FieldData
                    .GetAllAreaCoins()
                    .Where(scd => scd.CoinData.OwnerPlayerNo == playerNo)
                    .SelectMany(scd => scd.CoinData.StatusList.Items)
                    .All(status => status.RegisteredPlayerNo != playerNo))
                {
                    return true;
                }
            }
            else
            {
                // 全てのステータスが自分が付与したものではないなら実行
                // 細かい事を言うと、自分も付与予定だがまだ実行していない場合は残したい
                if (duelData.FieldData
                    .GetAllAreaCoins()
                    .Where(scd => scd.CoinData.OwnerPlayerNo != playerNo)
                    .SelectMany(scd => scd.CoinData.StatusList.Items)
                    .All(status => status.RegisteredPlayerNo != playerNo))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
