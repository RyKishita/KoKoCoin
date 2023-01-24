using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class ClearALLAreaStatus : Scripts.Coin.Body.Support.NoneTarget
    {
        public ClearALLAreaStatus(string coinName)
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

                    yield return GetLocalizedString(nameof(ClearALLAreaStatus), param);
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

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            foreach (var areaNo in duelManager.DuelData.GetAreaNos())
            {
                var areaData = duelManager.DuelData.FieldData.AreaDatas[areaNo];
                if (!areaData.Statuses.IsEmpty())
                {
                    areaData.Statuses.Clear();
                    duelManager.RegistDuelEventAction(new ActionEffectArea() { AreaNo = areaNo });
                }
            }
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
                .GetAreas()
                .SelectMany(area => area.Statuses.Items)
                .All(status => status.RegisteredPlayerNo != playerNo))
            {
                return true;
            }
            return false;
        }
    }
}
