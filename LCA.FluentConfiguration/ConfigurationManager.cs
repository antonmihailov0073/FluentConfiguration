using LCA.FluentConfiguration.Core;
using LCA.FluentConfiguration.Settings;

namespace LCA.FluentConfiguration
{
    public static class ConfigurationManager
    {
        public static IConfiguration Configuration { get; private set; }

        public static SettingsDictionary ConnectionStrings { get; private set; }
        

        private static void InitializeWith(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionStrings = configuration.ConnectionStrings;
        }
    }
}