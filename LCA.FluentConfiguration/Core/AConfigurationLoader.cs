using LCA.FluentConfiguration.Builders;

namespace LCA.FluentConfiguration.Core
{
    public abstract class AConfigurationLoader : IConfigurationLoader
    {
        protected abstract void OnLoading(ConfigurationBuilder builder);


        public IConfiguration Load()
        {
            var builder = new ConfigurationBuilder();
            OnLoading(builder);
            return builder.Build();
        }
    }
}
