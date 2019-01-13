using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using LCA.FluentConfiguration.Constants;
using LCA.FluentConfiguration.Core;
using LCA.FluentConfiguration.Settings;

namespace LCA.FluentConfiguration.Models
{
    internal sealed class Configuration : IConfiguration
    {
        private readonly JObject _jsonObject;


        public Configuration(JObject jsonObject)
        {
            _jsonObject = jsonObject;
            ConnectionStrings = SettingsLoader.Load<SettingsDictionary>(jsonObject, Defaults.Keys.ConnectionStrings);
        }


        public SettingsDictionary ConnectionStrings { get; }


        public TSection GetSection<TSection>(string key)
            where TSection : ASection
        {
            return _jsonObject.TryGetValue(key, out JToken jsonToken)
                ? jsonToken[key].ToObject<TSection>()
                : throw new KeyNotFoundException($"Section with key \"{key}\" was not found");
        }
    }
}
