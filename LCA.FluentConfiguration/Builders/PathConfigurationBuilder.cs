using LCA.FluentConfiguration.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace LCA.FluentConfiguration.Builders
{
    public class PathConfigurationBuilder
    {
        private readonly string _basePath;


        internal PathConfigurationBuilder(string basePath)
        {
            _basePath = basePath;
            IncludedFiles = new List<ConfigurationFile>();
        }


        internal List<ConfigurationFile> IncludedFiles { get; }


        public PathConfigurationBuilder IncludeJson(string filePath, bool isRequired = true)
        {
            var file = new ConfigurationFile
            {
                Path = Path.Combine(_basePath, filePath),
                IsRequired = isRequired
            };
            IncludedFiles.Add(file);
            return this;
        }

        public PathConfigurationBuilder IncludeJsons(params string[] filePaths)
        {
            Array.ForEach(filePaths, p => IncludeJson(p));
            return this;
        }
    }
}
