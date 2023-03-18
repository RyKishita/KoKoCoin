using Assets.Scripts.Coin.Effect;
using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Cysharp.Text;

namespace Assets.Scripts.Coin.Body.Guard
{
    public class Core : Body.Core, IHasValue
    {
        public Core(string coinName)
            : base(coinName)
        {

        }

        public override Defines.CoinType CoinType { get; } = Defines.CoinType.Guard;

        public int GuardValue { get; private set; }

        public Duel.DuelAnimation.Guard.GuardAnimation Animation { get; protected set; }

        public override IEnumerable<string> Summaries
        {
            get
            {
                foreach (var summary in base.Summaries)
                {
                    yield return summary;
                }

                if (0 < GuardValue)
                {
                    yield return Defines.GetLocalizedString(Defines.StringEnum.Protection) + GuardValue;
                }
            }
        }

        public int GetAppendValue(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            return GetCoinEffects<IEffectAppendValue>()
                    .Sum(effect => effect.GetAppendValue(duelData, selectedCoinData, GuardValue));
        }

        public int GetAppendProtection(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData directAttackSelectedCoinData)
        {
            return GetCoinEffects<IEffectAppendProtection>()
                    .Sum(effect => effect.GetAppendProtection(duelData, selectedCoinData, directAttackSelectedCoinData));
        }

        public bool IsFullGuard(DuelData duelData, SelectedCoinData selectedCoinData, SelectedCoinData directAttackSelectedCoinData)
        {
            return Effects
                    .Where(effect=>effect is IEffectGuardFull)
                    .Cast<IEffectGuardFull>()
                    .Any(effect => effect.IsFullGuard(duelData, selectedCoinData, directAttackSelectedCoinData));
        }

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

        public override void SetData(CSVImport data)
        {
            base.SetData(data);

            GuardValue = data.value;
        }

        public int GetValue() => GuardValue;
    }
}
