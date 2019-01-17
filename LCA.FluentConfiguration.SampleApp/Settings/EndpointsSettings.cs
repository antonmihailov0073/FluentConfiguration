using Newtonsoft.Json;

namespace LCA.FluentConfiguration.SampleApp.Settings
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EndpointsSettings
    {
        [JsonProperty("auth")]
        public string Auth { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }
    }
}
