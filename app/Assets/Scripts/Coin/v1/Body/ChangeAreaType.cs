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
    class ChangeAreaType : Scripts.Coin.Body.Support.Area
    {
        public ChangeAreaType(string coinName, Defines.AreaType areaType)
            : base(coinName)
        {
            this.AreaType = areaType;
        }

        public Defines.AreaType AreaType { get; }

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    int format = (AreaType == Defines.AreaType.Null) ? 0 : 1;

                    var param = new Dictionary<string, string>()
                    {
                        { "areatype", AreaType.ToString() },
                    };

                    yield return GetLocalizedString(nameof(ChangeAreaType), format, param);
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

            if (AreaType == Defines.AreaType.Null)
            {
                return duelData.FieldData.GetAreas().Any(areaData => areaData.Statuses.Has<Duel.AreaStatus.AreaStatusChangeField>());
            }
            else
            {
                return duelData.FieldData.GetAreas().Any(areaData => areaData.GetAreaType() != AreaType);
            }
        }

        public override List<int> GetTargetAreaNos(DuelData duelData)
        {
            if (AreaType == Defines.AreaType.Null)
            {
                return duelData.FieldData
                    .AreaDatas
                    .Where(pair => pair.Value.Statuses.Has<Duel.AreaStatus.AreaStatusChangeField>())
                    .Select(pair => pair.Key)
                    .ToList();
            }
            else
            {
                return duelData.FieldData
                    .AreaDatas
                    .Where(pair => pair.Value.GetAreaType() != AreaType)
                    .Select(pair => pair.Key)
                    .ToList();
            }
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var areaNo = (actionItem.SupportAction as Duel.SupportAction.SupportActionTargetArea).AreaNo;
            var playerNo = actionItem.GetPlayerNo();

            duelManager.RegistDuelEventAction(new ActionEffectArea() { AreaNo = areaNo });
            duelManager.RegistDuelEventAction(new ActionChangeAreaType()
            {
                ExecutePlayerNo = playerNo,
                AreaNo = areaNo,
                AreaType = AreaType
            });
            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 中央に近いエリアを優先
            var areaNos = duelData.FieldData.GetAreaNosOrderCenter().ToList();

            // デッキに入れている時点で関連するコインがあるはず。なので待たずに実行
            var resultAreaNos = areaNos.Where(areaNo => duelData.FieldData.AreaDatas[areaNo].GetAreaType() != AreaType).ToList();
            if (resultAreaNos.Any())
            {
                actionItem.SupportAction = new Duel.SupportAction.SupportActionTargetArea() { AreaNo = resultAreaNos.First() };
                return true;
            }

            if (bUseForce)
            {
                Utility.Functions.WriteLog(UnityEngine.LogType.Warning, 
                    nameof(ChangeAreaType),
                    actionItem.SelectedCoinData.GetCoin().Name,
                    "使用できる時点でいずれかは対象になるはず"
                );
            }

            return false;
        }
    }
}
