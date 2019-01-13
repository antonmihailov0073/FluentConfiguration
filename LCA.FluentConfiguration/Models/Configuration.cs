using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using LCA.FluentConfiguration.Constants;
using LCA.FluentConfiguration.Core;

namespace LCA.FluentConfiguration.Models
{
    internal sealed class Configuration : IConfiguration
    {
        private readonly JObject _jsonObject;


        public Configuration(JObject jsonObject)
        {
            _jsonObject = jsonObject;
            ConnectionStrings = LoadSettings<SettingsDictionary>(jsonObject, Defaults.Keys.ConnectionStrings);
        }


        public SettingsDictionary ConnectionStrings { get; }


        public TSection GetSection<TSection>(string name)
            where TSection : ASection
        {
            return _jsonObject[name].ToObject<TSection>();
        }
        

        private static TSettings LoadSettings<TSettings>(JObject jsonObject, string key)
            where TSettings : SettingsDictionary
        {
            return jsonObject.TryGetValue(key, out JToken token)
                ? (TSettings)Activator.CreateInstance(typeof(TSettings), token.ToObject<Dictionary<string, string>>())
                : null;
        }
    }
}
