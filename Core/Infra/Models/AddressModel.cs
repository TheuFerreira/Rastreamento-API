namespace Core.Infra.Models
{
    public class AddressModel
    {
        public AddressModel()
        {
            CEP = string.Empty;
            UF = string.Empty;
            City = string.Empty;
        }

        public int? AddressId { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
    }
}
