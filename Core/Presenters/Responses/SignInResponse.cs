using System.Text.Json.Serialization;

namespace Core.Presenters.Responses
{
    public class SignInResponse
    {
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}
