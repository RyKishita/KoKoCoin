using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class RemoveAreaStatus : Scripts.Coin.Body.Support.Area
    {
        public RemoveAreaStatus(string coinName, Duel.AreaStatus.IAreaStatus areaStatus)
            : base(coinName)
        {
            if (areaStatus == null) throw new ArgumentNullException(nameof(areaStatus));
            this.AreaStatus = areaStatus;
        }

        public Duel.AreaStatus.IAreaStatus AreaStatus { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    string status = Defines.GetLocalizedString(Defines.StringEnum.AreaStatus);
                    string name = AreaStatus.GetName();

                    var param = new Dictionary<string, string>()
                    {
                        { nameof(status), status},
                        { nameof(name), name},
                    };

                    yield return GetLocalizedString(nameof(RemoveAreaStatus), param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override List<int> GetTargetAreaNos(DuelData duelData)
        {
            return duelData.GetAreaNos().Where(areaNo => duelData.FieldData.AreaDatas[areaNo].Statuses.Has(AreaStatus)).ToList();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var areaNo = (actionItem.SupportAction as Duel.SupportAction.SupportActionTargetArea).AreaNo;

            var areaData = duelManager.DuelData.FieldData.AreaDatas[areaNo];
            areaData.Statuses.RemoveBy(AreaStatus);

            duelManager.RegistDuelEventAction(new ActionEffectArea() { AreaNo = areaNo });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 中央に近いエリアを優先
            var areaNos = duelData.FieldData.GetAreaNosOrderCenter().ToList();

            // 付与した人が自分ではないなら対象
            // 細かい事を言うと、自分も付与予定だがまだ実行していない場合は残したい
            var resultAreaNos = areaNos
                .Where(areaNo =>
                {
                    var status = duelData.FieldData.AreaDatas[areaNo].Statuses.GetItem(AreaStatus.GetType());
                    if (status == null) return false;
                    return status.RegisteredPlayerNo != actionItem.SelectedCoinData.CoinData.OwnerPlayerNo;
                })
                .ToList();
            if (resultAreaNos.Any())
            {
                actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetArea() { AreaNo = resultAreaNos.First() };
                return true;
            }
            if (bUseForce)
            {
                var reverseAreaNos = areaNos.ToList();
                reverseAreaNos.Reverse();
                resultAreaNos = reverseAreaNos.Where(areaNo => duelData.FieldData.AreaDatas[areaNo].Statuses.Has(AreaStatus)).ToList();
                if (resultAreaNos.Any())
                {
                    actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetArea() { AreaNo = resultAreaNos.First() };
                    return true;
                }

                Utility.Functions.WriteLog(UnityEngine.LogType.Warning,
                    nameof(RemoveAreaStatus),
                    actionItem.SelectedCoinData.CoinData.CoinName,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            return false;
        }
    }
}
