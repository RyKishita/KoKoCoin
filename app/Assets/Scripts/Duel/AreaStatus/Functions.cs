using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Localization.Settings;

namespace Assets.Scripts.Duel.AreaStatus
{
    internal static class Functions
    {
        public static string GetLocalizedString(string name)
        {
            return GetLocalizedString(name, new Dictionary<string, string>());
        }

        public static string GetLocalizedString(string name, Dictionary<string, string> param)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("AreaStatus", name, arguments: param);
        }

        public static string GetLocalizedStringName(string name)
        {
            return GetLocalizedStringName(name, new Dictionary<string, string>());
        }

        public static string GetLocalizedStringName(string name, Dictionary<string, string> param)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("AreaStatusName", name, arguments: param);
        }

        public static string GetLocalizedStringYomi(string name)
        {
            return GetLocalizedStringYomi(name, new Dictionary<string, string>());
        }

        public static string GetLocalizedStringYomi(string name, Dictionary<string, string> param)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("AreaStatusYomi", name, arguments: param);
        }
    }
}
