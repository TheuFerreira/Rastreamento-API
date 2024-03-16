namespace Core.Presenters.Responses
{
    public class BasicDeliveryResponse
    {
        public DateTime LastUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public BasicDeliveryAddressResponse Destiny { get; set; }

        public BasicDeliveryResponse() 
        {
            Destiny = new BasicDeliveryAddressResponse();
        }
    }

    public class BasicDeliveryAddressResponse
    {
        public BasicDeliveryAddressResponse()
        {
            CEP = string.Empty;
            UF = string.Empty;
            City = string.Empty;
            District = string.Empty;
            Street = string.Empty;
            Number = string.Empty;
            Complement = string.Empty;
        }

        public int AddressId { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
    }
}
