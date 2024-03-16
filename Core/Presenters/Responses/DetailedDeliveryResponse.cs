namespace Core.Presenters.Responses
{
    public class DetailedDeliveryResponse
    {
        public DateTime LastUpdate { get; set; }
        public DateTime CreatedDate { get; set; }

        public DetailedDeliveryAddressResponse Origin { get; set; }

        public int CurrentStatus { get; set; }

        public DetailedDeliveryAddressResponse Destiny { get; set; }

        public DetailedDeliveryResponse()
        {
            Destiny = new DetailedDeliveryAddressResponse();
            LastUpdate = DateTime.MinValue;
            CreatedDate = DateTime.MinValue;
            Origin = new DetailedDeliveryAddressResponse();
        }

        public DetailedDeliveryResponse(DateTime lastUpdate, DateTime createdDate, DetailedDeliveryAddressResponse origin, DetailedDeliveryAddressResponse destiny, int status)
        {
            CurrentStatus=status;
            Origin=origin;
            LastUpdate=lastUpdate;
            Destiny=destiny;
            CreatedDate=createdDate;
        }
    }

    public class DetailedDeliveryAddressResponse
    {
        public DetailedDeliveryAddressResponse()
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

