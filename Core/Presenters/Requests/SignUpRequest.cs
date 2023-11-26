using System.Text.Json.Serialization;

namespace Core.Presenters.Requests
{
    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }

        [JsonPropertyName("birth_date")]
        public DateOnly BirthDate { get; set; }
    }
}
