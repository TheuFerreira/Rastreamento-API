namespace Core.Presenters.Responses
{
    public class AddDeliveryResponse
    {
        public int Id {  get; set; }
        public string Code { get; set; }

        public AddDeliveryResponse()
        {
            Code = string.Empty;
        }
    }
}
