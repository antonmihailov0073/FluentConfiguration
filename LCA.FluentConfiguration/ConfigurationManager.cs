using LCA.FluentConfiguration.Core;
using LCA.FluentConfiguration.Models;

namespace LCA.FluentConfiguration
{
    public static class ConfigurationManager
    {
        public static IConfiguration Configuration { get; private set; }

        public static SettingsDictionary ConnectionStrings { get; private set; }
        

        private static void InitializeUsing(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionStrings = configuration.ConnectionStrings;
        }
    }
}