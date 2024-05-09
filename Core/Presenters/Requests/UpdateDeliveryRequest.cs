namespace Core.Presenters.Requests
{
    public class UpdateDeliveryRequest
    {
        public int DeliveryId { get; set; }
        public UpdateDeliveryAddressRequest Origin { get; set; }
        public UpdateDeliveryAddressRequest Destiny { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty;

        public UpdateDeliveryRequest()
        {
            Origin = new UpdateDeliveryAddressRequest();
            Destiny = new UpdateDeliveryAddressRequest();
        }
    }

    public class UpdateDeliveryAddressRequest
    {
        public string CEP { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
    }
}
