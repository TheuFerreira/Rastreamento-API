namespace Core.Infra.Models
{
    public class PositionDeliveryModel
    {
        public int? AddressId { get; set; }
        public int DeliveryId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
