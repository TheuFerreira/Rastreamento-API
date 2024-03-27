using System.Text.Json.Serialization;

namespace Core.Presenters.Responses
{
    public class GetSavedDeliveriesResponse
    {
        public GetSavedDeliveriesResponse(int deliveryId, int? courierId, string observation, string description, int addressOriginId, int addressDestinyId, string code, int status, DateTime lastUpdateTime, DateTime createdAt)
        {
            DeliveryId=deliveryId;
            CourierId=courierId;
            Observation=observation;
            Description=description;
            AddressOriginId=addressOriginId;
            AddressDestinyId=addressDestinyId;
            Code=code;
            Status=status;
            LastUpdateTime=lastUpdateTime;
            CreatedAt=createdAt;
        }

        public GetSavedDeliveriesResponse() 
        { 
            this.Observation  = String.Empty;
            this.Code  = String.Empty;
            this.Description  = String.Empty;
        }

        public int DeliveryId { get; set; }
        public int? CourierId { get; set; }
        public string Observation { get; set; }
        public string Description { get; set; }
        public int AddressOriginId { get; set; }
        public int AddressDestinyId { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        [JsonPropertyName("last_update_time")]
        public DateTime LastUpdateTime { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }


    
       
    }
}
