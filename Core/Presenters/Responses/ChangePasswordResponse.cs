namespace Core.Presenters.Responses
{
    public class ChangePasswordResponse
    {
        public string message { get; set; }
        public bool status { get; set; }

        public ChangePasswordResponse()
        {
            message = string.Empty;
            status = false;
        }
    }
}
