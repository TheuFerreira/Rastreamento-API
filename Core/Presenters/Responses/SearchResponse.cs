using System.Text.Json.Serialization;

namespace Core.Presenters.Responses
{
    public class SearchResponse
    {
        public SearchResponse()
        {
            Description = string.Empty;
        }

        [JsonPropertyName("id_delivery")]
        public int DeliveryId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("last_update_time")]
        public DateTime LastUpdateTime { get; set; }
    }
}
