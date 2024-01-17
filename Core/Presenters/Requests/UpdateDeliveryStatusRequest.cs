using System.Text.Json.Serialization;

namespace Core.Presenters.Requests
{
    public class UpdateDeliveryStatusRequest
    {
        [JsonPropertyName("delivery_id")]
        public int DeliveryId { get; set; }
        public int Status { get; set; }
    }
}
