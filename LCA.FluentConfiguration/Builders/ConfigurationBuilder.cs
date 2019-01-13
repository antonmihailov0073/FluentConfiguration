using System.Collections.Generic;
using System.IO;
using System;
using LCA.FluentConfiguration.Constants;
using LCA.FluentConfiguration.Core;
using LCA.FluentConfiguration.Models;
using LCA.FluentConfiguration.Helpers;

namespace LCA.FluentConfiguration.Builders
{
    public class ConfigurationBuilder
    {
        public static readonly string BaseDirectory;
        public static readonly string StandardConfigsPath;


        static ConfigurationBuilder()
        {
            BaseDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            StandardConfigsPath = Path.Combine(BaseDirectory, Defaults.Folders.Configurations);
        }

        public ConfigurationBuilder()
        {
            IncludedFiles = new List<ConfigurationFile>();
        }


        internal List<ConfigurationFile> IncludedFiles { get; set; }


        public ConfigurationBuilder ForPath(string basePath, Action<PathConfigurationBuilder> builderConfigutator)
        {
            var pathBuilder = new PathConfigurationBuilder(basePath);
            builderConfigutator(pathBuilder);
            IncludedFiles.AddRange(pathBuilder.IncludedFiles);
            return this;
        }

        public IConfiguration Build()
        {
            var json = JsonHelper.LoadFromFiles(IncludedFiles);
            return new Configuration(json);
        }
    }
}
