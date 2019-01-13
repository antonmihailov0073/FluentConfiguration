using System.Collections.Generic;

namespace LCA.FluentConfiguration.Models
{
    public class SettingsDictionary
    {
        public SettingsDictionary(Dictionary<string, string> settings)
        {
            Settings = settings;
        }


        protected Dictionary<string, string> Settings { get; }


        public string this[string key] => Settings.TryGetValue(key, out string value)
            ? value
            : null;
    }
}
