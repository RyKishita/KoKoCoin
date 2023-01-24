using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class Seizure : Scripts.Coin.Body.Support.NoneTarget
    {
        public Seizure(string coinName, int num)
            : base(coinName)
        {
            this.num = num;
        }

        readonly int num;

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    var param = new Dictionary<string, string>()
                    {
                        { nameof(num), num.ToString()},
                    };

                    yield return GetLocalizedString(nameof(Seizure), param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override async UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var playerNo = actionItem.GetPlayerNo();

            var coinIDs = duelManager.DuelData.FieldData
                            .GetAllAreaCoins()
                            .Where(scd => scd.CoinData.OwnerPlayerNo == playerNo)
                            .Select(scd => scd.CoinData.ID);

            var targetObjects = duelManager.GetObjectsByCoin(coinIDs).ToList();

            await duelManager.ProcessExtraWinAsync(playerNo, Main.Seizure.name, targetObjects);
        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;
            int count = duelData.FieldData
                        .GetAllAreaCoins()
                        .Where(scd => scd.CoinData.OwnerPlayerNo == selectedCoinData.CoinData.OwnerPlayerNo)
                        .Count();
            return num <= count;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            // 使用できるということは、条件をすでに満たしている
            return true;
        }
    }
}
