using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using Assets.Scripts.Coin.Effect;
using UnityEngine.Localization.Settings;
using Cysharp.Text;

namespace Assets.Scripts.Coin.Body
{
    public abstract class Core
    {
        public Core(string coinName)
        {
            this.coinName = coinName;
        }

        protected readonly string coinName;

        public virtual IEnumerable<string> Summaries
        {
            get
            {
                yield return $"[{CoinTypeDisplayString}]";

                if (CoinTag != Defines.CoinTag.None)
                {
                    yield return $"[{CoinTagDisplayString}]";
                }
            }
        }

        public abstract Defines.CoinType CoinType { get; }

        public string CoinTypeDisplayString => Defines.GetLocalizedString(CoinType);

        public Defines.CoinTag CoinTag { get; private set; }

        public string CoinTagDisplayString => Defines.GetLocalizedString(CoinTag);

        public IEnumerable<string> GetExplains()
        {
            var explains = Explains.ToArray();
            foreach (int index in Enumerable.Range(0, explains.Length))
            {
                yield return Defines.GetNoText(index) + explains[index];
            }
        }

        /// <summary>
        /// 効果
        /// </summary>
        public virtual Scripts.Coin.Effect.IEffect[] Effects { get; } = new Scripts.Coin.Effect.IEffect[] { };

        /// <summary>
        /// 説明文
        /// </summary>
        protected virtual IEnumerable<string> Explains => Effects.Select(effect => effect.Explain);

        public virtual bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (selectedCoinData.IsValidEffect())
            {
                if (Effects
                    .Where(effect => effect is IEffectUsable)
                    .Cast<IEffectUsable>()
                    .Any(effect => !effect.IsUsable(duelData, selectedCoinData))) return false;
            }

            return true;
        }

        public bool IsQueryExtraCost()
        {
            return !IsValidExtraCost(new List<CoinData>());
        }

        public bool IsValidExtraCost(List<CoinData> coinDatas)
        {
            return Effects.Where(effect => effect is IEffectNeedExtraCost).Cast<IEffectNeedExtraCost>().All(effect => effect.IsValidExtraCost(coinDatas));
        }

        public IEnumerable<T> GetCoinEffects<T>() where T : IEffect
        {
            return Effects.Where(effect => effect is T).Cast<T>();
        }

        IEnumerable<IEffectTriggerEvent> GetReceiveEvents(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return GetCoinEffects<IEffectTriggerEvent>()
                    .Where(effect => !duelData.IsExecutedEffect(selectedCoinData, effect))
                    .Where(effect => effect.IsReceiveEvent(duelData, selectedCoinData, duelEvent));
        }

        public bool IsReceiveEvent(DuelData duelData, SelectedCoinData selectedCoinData, After duelEvent)
        {
            return GetReceiveEvents(duelData, selectedCoinData, duelEvent).Any();
        }

        public virtual async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, After duelEvent)
        {
            var receiveEvents = GetReceiveEvents(duelManager.DuelData, selectedCoinData, duelEvent).ToList();
            if (!receiveEvents.Any()) return;

            foreach (var effect in receiveEvents)
            {
                await effect.ReceiveEventAsync(duelManager, selectedCoinData, duelEvent);
                duelManager.DuelData.RegistExecutedEffect(selectedCoinData, effect);
            }
        }

        public bool IsCoinType(Defines.CoinType selectCoinType)
        {
            if (CoinType == Defines.CoinType.None) return selectCoinType == Defines.CoinType.None;
            return selectCoinType.HasFlag(CoinType);
        }

        public bool IsUseForce => Effects.Any(effect => effect is IEffectUseForce);

        public virtual IEnumerable<string> GetPrefabNames()
        {
            yield break;
        }

        public IEnumerable<string> GetCopiedCoinNames()
        {
            return Effects.SelectMany(effect => effect.GetCopiedCoinNames());
        }

        protected string GetLocalizedString(string name)
        {
            return GetLocalizedString(name, new Dictionary<string, string>());
        }

        protected string GetLocalizedString(string name, int format)
        {
            return GetLocalizedString(ZString.Format("{0}{1}", name, format));
        }

        protected string GetLocalizedString(string name, Dictionary<string, string> param)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("CoinBody", name, arguments: param);
        }

        protected string GetLocalizedString(string name, int format, Dictionary<string, string> param)
        {
            return GetLocalizedString(ZString.Format("{0}{1}", name, format), param);
        }

        public IEnumerable<string> GetSearchTexts()
        {
            foreach (var summary in Summaries)
            {
                yield return summary;
            }
            foreach (var explain in Explains)
            {
                yield return explain;
            }
        }

        public bool? IsAdvantageEffect_ProgressedTurn(DuelData duelData)
        {
            int trueNum = Effects.Count(effect => effect.IsAdvantageProgressedTurn(duelData) == true);
            int falseNum = Effects.Count(effect => effect.IsAdvantageProgressedTurn(duelData) == false);
            if (trueNum == falseNum) return null;
            return trueNum < falseNum;
        }

        public IEnumerable<IEffectInterceptDuelAction> GetInterceptDuelAction(DuelData duelData, SelectedCoinData selectedCoinData, Duel.DuelEvent.Action duelEventAction) =>
            GetCoinEffects<IEffectInterceptDuelAction>()
            .Where(effect => effect.IsInterceptDuelAction(duelData, selectedCoinData, duelEventAction));

        public CoinEffectReferenceItem GetCoinEffectReferenceItem(IEffect effect)
        {
            foreach (var effectIndex in Enumerable.Range(0, Effects.Length))
            {
                if (ReferenceEquals(Effects[effectIndex], effect))
                {
                    return new CoinEffectReferenceItem() { EffectIndex = effectIndex };
                }
            }
            return null;
        }

        public virtual void SetData(CSVImport data)
        {
            CoinTag = data.GetCoinTag();
        }
    }
}
