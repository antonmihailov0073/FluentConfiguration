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


        public PathConfigurationBuilder IncludeJson(string filePath, bool isRequired = true, bool watch = false)
        {
            var file = new ConfigurationFile
            {
                Path = Path.Combine(_basePath, filePath),
                IsRequired = isRequired,
                Watch = watch
            };
            IncludedFiles.Add(file);
            return this;
        }
    }
}
