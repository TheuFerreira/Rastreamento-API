namespace Core.Presenters.Requests
{
    public class AddDeliveryRequest
    {
        public AddDeliveryRequest()
        {
            Observation = string.Empty;
            Description = string.Empty;
            Origin = new AddDeliveryAddressRequest();
            Destiny = new AddDeliveryAddressRequest();
        }

        public string Observation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AddDeliveryAddressRequest Origin { get; set; }
        public AddDeliveryAddressRequest Destiny { get; set; }
    }

    public class AddDeliveryAddressRequest
    {
        public AddDeliveryAddressRequest()
        {
            CEP = string.Empty;
            UF = string.Empty;
            City = string.Empty;
            District = string.Empty;
            Street = string.Empty;
            Number = string.Empty;
            Complement = string.Empty;
        }

        public string CEP { get; set; }
        public string UF { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
    }
}

