using LCA.FluentConfiguration.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace LCA.FluentConfiguration.Helpers
{
    public static class JsonHelper
    {
        private readonly static JsonMergeSettings _mergeSettings;


        static JsonHelper()
        {
            _mergeSettings = new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Replace,
                MergeNullValueHandling = MergeNullValueHandling.Merge
            };
        }


        public static JObject LoadFromFiles(IEnumerable<ConfigurationFile> includedFiles)
        {
            var jsonObject = new JObject();
            foreach (var file in includedFiles)
            {
                MergeFileIntoJson(jsonObject, file);
                var environmentFile = new ConfigurationFile
                {
                    Path = EnvironmentHelper.GetEnvironmentSpecificFile(file.Path),
                    IsRequired = false,
                    Watch = file.Watch
                };
                MergeFileIntoJson(jsonObject, environmentFile);
            }
            return jsonObject;
        }


        private static void MergeFileIntoJson(JObject jsonObject, ConfigurationFile file)
        {
            if (!file.IsRequired && !File.Exists(file.Path)) return;
            var fileContent = File.ReadAllText(file.Path);
            var parsedJson = JObject.Parse(fileContent);
            jsonObject.Merge(parsedJson, _mergeSettings);
        }
    }
}
