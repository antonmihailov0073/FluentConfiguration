using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace LCA.FluentConfiguration.Settings
{
    internal static class SettingsLoader
    {
        public static TSettings Load<TSettings>(JObject jsonObject, string key)
            where TSettings : SettingsDictionary
        {
            if (!jsonObject.TryGetValue(key, out JToken jsonToken)) return null;
            var jsonDict = jsonToken.ToObject<Dictionary<string, string>>();
            return Activator.CreateInstance(typeof(TSettings), jsonDict) as TSettings;
        }
    }
}
