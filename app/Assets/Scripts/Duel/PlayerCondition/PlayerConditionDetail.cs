using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using System;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public abstract class PlayerConditionDetail
    {
        public abstract string DisplayName { get; }

        public abstract bool IsGood(PlayerCondition playerCondition);

        public abstract Defines.ParticleType GetParticleType(PlayerCondition playerCondition);

        public abstract string MakeExplain();

        public abstract string MakeExplain(DuelData duelData, PlayerCondition playerCondition);

        public static string GetLocalizedString(string name)
        {
            return GetLocalizedString(name, new Dictionary<string, string>());
        }

        public static string GetLocalizedString(string name, int format)
        {
            return GetLocalizedString(ZString.Format("{0}{1}", name, format));
        }

        public static string GetLocalizedString(string name, Dictionary<string, string> param)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("PlayerCondition", name, arguments:param);
        }

        public static string GetLocalizedString(string name, int format, Dictionary<string, string> param)
        {
            return GetLocalizedString(ZString.Format("{0}{1}", name, format), param);
        }

        public static string GetLocalizedStringName(string name)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("PlayerConditionName", name);
        }

        public static string GetLocalizedStringName(string name, int format)
        {
            return GetLocalizedStringName(ZString.Format("{0}{1}", name, format));
        }

        public enum ValueTypeEnum
        {
            Override,
            Marge,
        }

        public virtual ValueTypeEnum ValueType => ValueTypeEnum.Marge;
    }
}
