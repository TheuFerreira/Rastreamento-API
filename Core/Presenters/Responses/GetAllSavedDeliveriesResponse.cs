using System.Text.Json.Serialization;

namespace Core.Presenters.Responses
{
    public class GetAllSavedDeliveriesResponse
    {
        [JsonPropertyName("delivery_id")]
        public int DeliveryId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
