using System.Text.Json.Serialization;

namespace Core.Presenters.Responses
{
    public class GetUserDeliveriesResponse
    {
        [JsonPropertyName("id_delivery")]
        public int DeliveryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public GetUserDeliveriesAddressResponse Origin { get; set; }
        public GetUserDeliveriesAddressResponse Destiny { get; set; }
        public int Status { get; set; }

        [JsonPropertyName("last_update_time")]
        public DateTime LastUpdateTime { get; set; }

        public GetUserDeliveriesResponse()
        {
            Origin = new GetUserDeliveriesAddressResponse();
            Destiny = new GetUserDeliveriesAddressResponse();
        }
    }

    public class GetUserDeliveriesAddressResponse
    {
        public string UF { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}
