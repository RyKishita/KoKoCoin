using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class RemoveCoinStatusALLSetCoinsByPlayer : Scripts.Coin.Body.Support.NoneTarget
    {
        public RemoveCoinStatusALLSetCoinsByPlayer(string coinName, bool bCoinOwner, Duel.CoinStatus.ICoinStatus targetStatus)
            : base(coinName)
        {
            if (targetStatus == null) throw new ArgumentNullException(nameof(targetStatus));
            IsTargetCoinOwner = bCoinOwner;
            TargetCoinStatus = targetStatus;
        }

        public bool IsTargetCoinOwner { get; }
        public Duel.CoinStatus.ICoinStatus TargetCoinStatus { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string player = Defines.GetLocalizedString(IsTargetCoinOwner ? Defines.StringEnum.You : Defines.StringEnum.Opponent);
                    string settedcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);
                    string name = TargetCoinStatus.ToString();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(player), player},
                        { nameof(settedcoin), settedcoin},
                        { nameof(name), name},
                    };

                    yield return GetLocalizedString(nameof(RemoveCoinStatusALLSetCoinsByPlayer), param);
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
            var playerNos = IsTargetCoinOwner
                ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();
            return duelData.FieldData
                .GetCoinByHasStatus(TargetCoinStatus)
                .Where(scd => playerNos.Contains(scd.CoinData.OwnerPlayerNo))
                .Any();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var duelData = duelManager.DuelData;
            var playerNo = actionItem.GetPlayerNo();

            var targetPlayerNos = IsTargetCoinOwner
                                    ? new List<int>() { playerNo }
                                    : duelData.GetOtherTeamPlayerNos(playerNo).ToList();

            var coinIDs = duelData.FieldData
                            .GetAllAreaCoins()
                            .Where(scd => targetPlayerNos.Contains(scd.CoinData.OwnerPlayerNo))
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

            if (IsTargetCoinOwner)
            {
                if (duelData.FieldData
                    .GetAllAreaCoins()
                    .Where(scd => scd.CoinData.OwnerPlayerNo == playerNo)
                    .SelectMany(scd => scd.CoinData.StatusList.Items)
                    .Where(status => status.IsMatch(TargetCoinStatus))
                    .All(status => status.RegisteredPlayerNo != playerNo))
                {
                    return true;
                }
            }
            else
            {
                if (duelData.FieldData
                    .GetAllAreaCoins()
                    .Where(scd => scd.CoinData.OwnerPlayerNo != playerNo)
                    .SelectMany(scd => scd.CoinData.StatusList.Items)
                    .Where(status => status.IsMatch(TargetCoinStatus))
                    .All(status => status.RegisteredPlayerNo != playerNo))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
