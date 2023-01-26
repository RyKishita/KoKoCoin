using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Localization.Settings;

namespace Assets.Scripts.Duel.CoinStatus
{
    internal static class Functions
    {
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
            return LocalizationSettings.StringDatabase.GetLocalizedString("CoinStatus", name, arguments: param);
        }

        public static string GetLocalizedString(string name, int format, Dictionary<string, string> param)
        {
            return GetLocalizedString(ZString.Format("{0}{1}", name, format), param);
        }

        public static string GetLocalizedStringName(string name)
        {
            return GetLocalizedStringName(name, new Dictionary<string, string>());
        }

        public static string GetLocalizedStringName(string name, int format)
        {
            return GetLocalizedStringName(ZString.Format("{0}{1}", name, format));
        }

        public static string GetLocalizedStringName(string name, Dictionary<string, string> param)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("CoinStatusName", name, arguments: param);
        }

        public static string GetLocalizedStringName(string name, int format, Dictionary<string, string> param)
        {
            return GetLocalizedStringName(ZString.Format("{0}{1}", name, format), param);
        }
    }
}
