using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Cysharp.Text;

namespace Assets.Scripts.Coin.Body.DirectAttack
{
    public class Core : Body.Core, IHasValue
    {
        public Core(string coinName)
            : base(coinName)
        {

        }

        public override Defines.CoinType CoinType { get; } = Defines.CoinType.DirectAttack;

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            return base.IsUsable(duelData, selectedCoinData) &&
                duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo)
                .Any(targetPlayerNo =>
                    Range.IsInRange(duelData, targetPlayerNo, selectedCoinData) &&
                    duelData.GetAreaTypeIsPlayerLocated(targetPlayerNo) != Defines.AreaType.Safe);
        }

        public int DirectAttackValue { get; private set; }

        IRange _Range;
        public virtual IRange Range
        {
            get
            {
                if (_Range == null)
                {
                    _Range = new RangeAny(CoinTag);
                }
                return _Range;
            }
        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                return Range.Explains.Concat(Effects.Select(effect => effect.Explain));
            }
        }

        public override IEnumerable<string> Summaries
        {
            get
            {
                foreach (var summary in base.Summaries)
                {
                    yield return summary;
                }
                if (0 < DirectAttackValue)
                {
                    yield return Defines.GetLocalizedString(Defines.StringEnum.Damage) + DirectAttackValue;
                }
            }
        }

        public Duel.DuelAnimation.DirectAttack.DirectAttackAnimation Animation { get; protected set; }

        public override IEnumerable<string> GetPrefabNames()
        {
            foreach (var prefabName in base.GetPrefabNames())
            {
                yield return prefabName;
            }

            if (Animation != null)
            {
                foreach(var prefabName in Animation.GetPrefabNames())
                {
                    yield return prefabName;
                }
            }
        }

        public int GetAppendValue(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            return GetCoinEffects<Effect.IEffectAppendValue>().Sum(effect => effect.GetAppendValue(duelData, selectedCoinData, DirectAttackValue));
        }

        public override void SetData(CSVImport data)
        {
            base.SetData(data);

            DirectAttackValue = data.value;
        }

        public int GetValue() => DirectAttackValue;
    }
}
