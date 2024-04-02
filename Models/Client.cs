using System.Text.Json.Serialization;

namespace om_auth_api.Models
{
    public class Client
    {
        [JsonPropertyName("ct_Num")]
        public string CT_Num { get; set; }
        [JsonPropertyName("ct_Intitule")]
        public string CT_Intitule { get; set; }
    }
}
