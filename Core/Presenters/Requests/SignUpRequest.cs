namespace Core.Presenters.Requests
{
    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
