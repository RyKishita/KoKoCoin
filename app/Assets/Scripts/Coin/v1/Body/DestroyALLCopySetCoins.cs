using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class DestroyALLCopySetCoins : Scripts.Coin.Body.Support.NoneTarget
    {
        public DestroyALLCopySetCoins(string coinName)
            : base(coinName)
        {
        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string field = Defines.GetLocalizedString(Defines.CoinPosition.Field);
                    string settedcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);
                    string trash = Defines.GetLocalizedString(Defines.CoinPosition.Trash);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(field), field },
                        { nameof(settedcoin), settedcoin },
                        { nameof(trash), trash },
                    };

                    yield return GetLocalizedString(nameof(DestroyALLCopySetCoins), param);
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
            return duelData.FieldData.GetAllAreaCoins().Where(scc => scc.CoinData.IsCopied()).Any();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var duelData = duelManager.DuelData;

            duelManager.RegistDuelEventAction(new ActionMoveCoinsToPlayer()
            {
                CoinIDs = duelData.FieldData
                            .GetAllAreaCoins()
                            .Where(scd => scd.CoinData.IsCopied())
                            .Select(scc => scc.CoinData.ID)
                            .ToList(),
                DstCoinPosition = Defines.CoinPosition.Trash
            });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            var playerNo = actionItem.GetPlayerNo();

            // 複製が全て他のユーザーの物なら実行
            if (duelData.FieldData
                .GetAllAreaCoins()
                .All(scd => scd.CoinData.IsCopied() && scd.CoinData.OwnerPlayerNo != playerNo))
            {
                return true;
            }

            return false;
        }
    }
}
