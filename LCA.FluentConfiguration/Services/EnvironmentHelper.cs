using System.IO;

namespace LCA.FluentConfiguration.Providers
{
    internal static class EnvironmentHelper
    {
        static EnvironmentHelper()
        {
            BuildEnvironment = GetEnvironment();
        }
        

        public static string BuildEnvironment { get; }


        public static string GetEnvironmentSpecificFile(string file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var fileExtension = Path.GetExtension(file);
            var directory = Path.GetDirectoryName(file);
            return Path.Combine(directory, $"{fileName}.{BuildEnvironment}{fileExtension}");
        }


        private static string GetEnvironment()
        {
            #if DEBUG
                return "debug";
            #elif TEST
                return "test";
            #elif STAGE
                return "stage";
            #elif RELEASE
                return "release";
            #endif
        }
    }
}
