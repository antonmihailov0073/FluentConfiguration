namespace LCA.FluentConfiguration.Core
{
    public interface IConfiguration
    {
        IConfiguration this[string path] { get; }


        TModel To<TModel>();
    }
}
