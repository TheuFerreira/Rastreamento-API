using System.Text.Json.Serialization;

namespace Core.Presenters.Responses
{
    public class UserInfoResponse
    {
        [JsonPropertyName("full_name")]
        public string FullName {  get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;

        [JsonPropertyName("birth_date")]
        public DateOnly BirthDate { get; set; }
    }
}
