using System.Text.Json.Serialization;

namespace Core.Presenters.Responses
{
    public class UserInfoResponse
    {
        [JsonPropertyName("fullname")]
        public string FullName {  get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;

        [JsonPropertyName("birth_date")]
        public string BirthDate { get; set; } = string.Empty;
    }
}
