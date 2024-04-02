using System.Text.Json.Serialization;

namespace om_auth_api.Models
{
    public class Article
    {
        [JsonPropertyName("ar_Ref")]
        public string AR_Ref { get; set; }
        [JsonPropertyName("ar_Design")]
        public string AR_Design { get; set; }
    }
}
