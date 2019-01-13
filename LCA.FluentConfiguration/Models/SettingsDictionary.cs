using System.Collections.Generic;

namespace LCA.FluentConfiguration.Models
{
    public class SettingsDictionary : Dictionary<string, string>
    {
        public SettingsDictionary(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
        }
    }
}
