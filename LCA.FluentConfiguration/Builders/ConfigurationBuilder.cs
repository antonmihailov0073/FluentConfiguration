using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using LCA.FluentConfiguration.Constants;
using LCA.FluentConfiguration.Core;
using LCA.FluentConfiguration.Providers;
using LCA.FluentConfiguration.Models;

namespace LCA.FluentConfiguration.Builders
{
    public class ConfigurationBuilder
    {
        public static readonly string BaseDirectory;
        public static readonly string StandartConfigsPath;


        private string _basePath;


        static ConfigurationBuilder()
        {
            BaseDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            StandartConfigsPath = Path.Combine(BaseDirectory, Defaults.Folders.Configurations);
        }

        public ConfigurationBuilder()
        {
            AddedFiles = new List<string>();
        }


        internal List<string> AddedFiles { get; set; }


        public ConfigurationBuilder SetBasePath(string path)
        {
            _basePath = path;
            return this;
        }

        public ConfigurationBuilder UseStandardSharedPath()
        {
            return SetBasePath(Path.Combine(StandartConfigsPath, Defaults.Folders.Shared));
        }

        public ConfigurationBuilder UseStandardLocalPath()
        {
            return SetBasePath(Path.Combine(StandartConfigsPath, Defaults.Folders.Local));
        }

        public ConfigurationBuilder IncludeJson(string filePath)
        {
            AddedFiles.Add(Path.Combine(_basePath, filePath));
            return this;
        }

        public ConfigurationBuilder IncludeJsons(params string[] filePaths)
        {
            Array.ForEach(filePaths, p => IncludeJson(p));
            return this;
        }


        internal IConfiguration Build()
        {
            var json = LoadJson();
            return new Configuration(json);
        }


        private JObject LoadJson()
        {
            var jsonView = new JObject();
            foreach (var path in AddedFiles)
            {
                MergeJson(jsonView, path, true);
                MergeJson(jsonView, EnvironmentHelper.GetEnvironmentSpecificFile(path), false);
            }
            return jsonView;
        }


        private static void MergeJson(JObject jsonView, string path, bool required)
        {
            if (!required && !File.Exists(path)) return;
            var parsedJson = JObject.Parse(File.ReadAllText(path));
            jsonView.Merge(parsedJson, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Replace,
                MergeNullValueHandling = MergeNullValueHandling.Merge
            });
        }
    }
}
