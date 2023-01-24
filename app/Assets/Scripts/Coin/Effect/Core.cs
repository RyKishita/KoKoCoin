using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization.Settings;
using Cysharp.Text;

namespace Assets.Scripts.Coin.Effect
{
    public abstract class Core : IEffect
    {
        public abstract string Explain { get; }

        public virtual IEnumerable<string> GetCopiedCoinNames()
        {
            yield break;
        }

        protected string GetEffectLocalizedString(string name)
        {
            return GetEffectLocalizedString(name, new Dictionary<string, string>());
        }

        protected string GetEffectLocalizedString(string name, int format)
        {
            return GetEffectLocalizedString(ZString.Format("{0}{1}", name, format));
        }

        protected string GetEffectLocalizedString(string name, Dictionary<string, string> param)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("CoinEffect", name, arguments: param);
        }

        protected string GetEffectLocalizedString(string name, int format, Dictionary<string, string> param)
        {
            return GetEffectLocalizedString(ZString.Format("{0}{1}", name, format), param);
        }

        public virtual bool? IsAdvantageProgressedTurn(DuelData duelData)
        {
            return null;
        }

        public virtual bool IsProcessedOnArea()
        {
            return true;
        }

        public virtual bool IsAdvantage(DuelData duelData)
        {
            return true;
        }
    }
}
