namespace Core.Infra.Models
{
    public class DeliveryModel
    {
        public int DeliveryId { get; set; }
        public int? CourierId { get; set; }
        public string Observation { get; set; }
        public string Description { get; set; }
        public int AddressOriginId { get; set; }
        public int AddressDestinyId { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime CreatedAt { get; set; }


        public DeliveryModel()
        {
            Observation = string.Empty;
            Description = string.Empty;
            Code = string.Empty;
        }

        public DeliveryModel(string observation, string description, int addressOriginId, int addressDestinationId, int courierId)
        {
            Observation = observation;
            Description = description;
            AddressOriginId = addressOriginId;
            AddressDestinyId = addressDestinationId;
            Code = string.Empty;
            CourierId = courierId;
        }
    }
}
