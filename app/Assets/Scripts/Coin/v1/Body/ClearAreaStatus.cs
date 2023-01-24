using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class ClearAreaStatus : Scripts.Coin.Body.Support.Area
    {
        public ClearAreaStatus(string coinName)
            : base(coinName)
        {

        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string status = Defines.GetLocalizedString(Defines.StringEnum.AreaStatus);

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(status), status},
                    };

                    yield return GetLocalizedString(nameof(ClearAreaStatus), param);
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
            return duelData.FieldData.GetAreas().Any(areaData => !areaData.Statuses.IsEmpty());
        }

        public override List<int> GetTargetAreaNos(DuelData duelData)
        {
            return duelData.GetAreaNos().Where(areaNo => !duelData.FieldData.AreaDatas[areaNo].Statuses.IsEmpty()).ToList();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var areaNo = (actionItem.SupportAction as Duel.SupportAction.SupportActionTargetArea).AreaNo;

            var areaData = duelManager.DuelData.FieldData.AreaDatas[areaNo];
            areaData.Statuses.Clear();

            duelManager.RegistDuelEventAction(new ActionEffectArea() { AreaNo = areaNo });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            int playerNo = actionItem.SelectedCoinData.CoinData.OwnerPlayerNo;

            // 中央に近いエリアを優先
            var areaNos = duelData.FieldData.GetAreaNosOrderCenter().ToList();

            // 付与した人が自分ではないなら対象
            // 細かい事を言うと、自分も付与予定だがまだ実行していない場合は残したい
            var resultAreaNos = areaNos.Where(areaNo =>
            {
                var statuses = duelData.FieldData.AreaDatas[areaNo].Statuses;
                if (statuses.IsEmpty()) return false;
                if (statuses.Items.All(status => status.RegisteredPlayerNo == playerNo)) return false;
                return true;
            }).ToList();
            if (resultAreaNos.Any())
            {
                actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetArea() { AreaNo = resultAreaNos.First() };
                return true;
            }

            if (bUseForce)
            {
                // 自分が付与したものに対する実行。外側から確認
                var reverseAreaNos = areaNos.ToList();
                reverseAreaNos.Reverse();
                resultAreaNos = reverseAreaNos.Where(areaNo => !duelData.FieldData.AreaDatas[areaNo].Statuses.IsEmpty()).ToList();
                if (resultAreaNos.Any())
                {
                    actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetArea() { AreaNo = resultAreaNos.First() };
                    return true;
                }

                Utility.Functions.WriteLog(UnityEngine.LogType.Warning, 
                    nameof(ClearAreaStatus),
                    actionItem.SelectedCoinData.GetCoin().Name,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            return false;
        }
    }
}
