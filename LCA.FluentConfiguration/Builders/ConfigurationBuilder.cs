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
        public static readonly string ExecutingDirectory;
        public static readonly string DefaultConfigurationsPath;


        static ConfigurationBuilder()
        {
            ExecutingDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            DefaultConfigurationsPath = Path.Combine(ExecutingDirectory, Defaults.Folders.Configurations);
        }

        public ConfigurationBuilder()
        {
            IncludedFiles = new List<ConfigurationFile>();
        }


        internal List<ConfigurationFile> IncludedFiles { get; }


        public ConfigurationBuilder ForPath(string basePath, Action<PathConfigurationBuilder> pathBuilderConfigutator)
        {
            var pathBuilder = new PathConfigurationBuilder(basePath);
            pathBuilderConfigutator(pathBuilder);
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
