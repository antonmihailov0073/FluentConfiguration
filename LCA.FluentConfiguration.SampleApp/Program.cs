using LCA.FluentConfiguration.Builders;
using LCA.FluentConfiguration.SampleApp.Settings;
using System;

namespace LCA.FluentConfiguration.SampleApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .ForPath("Configurations", pathBuilder =>
                {
                    pathBuilder.IncludeJson("endpoints.json")
                        .IncludeJson("optional.json", isRequired: false);
                })
                .Build();

            ConfigurationManager.InitializeUsing(configuration);

            var endpoints = configuration["endpoints"].To<EndpointsSettings>();
            Console.WriteLine(endpoints.Notes);

            Console.WriteLine(configuration["endpoints.auth"].To<string>());

            Console.ReadKey(true);
        }
    }
}
