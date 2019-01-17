using LCA.FluentConfiguration.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace LCA.FluentConfiguration.Models
{
    public class Configuration : IConfiguration
    {
        private readonly JToken _json;


        internal Configuration(JToken json)
        {
            _json = json;
        }


        public IConfiguration this[string path]
        {
            get
            {
                JToken token = null;
                try
                {
                    token = _json.SelectToken(path, true);
                }
                catch (JsonException)
                {
                    return null;
                }
                return new Configuration(token);
            }
        }

        
        public TModel To<TModel>()
        {
            return _json.ToObject<TModel>();
        }
    }
}
