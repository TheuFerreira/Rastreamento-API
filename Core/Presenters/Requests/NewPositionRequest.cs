using System.Text.Json.Serialization;

namespace Core.Presenters.Requests
{
    public class NewPositionRequest
    {
        [JsonPropertyName("delivery_id")]
        public int DeliveryId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
