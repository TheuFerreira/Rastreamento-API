using System.Text.Json.Serialization;

namespace Core.Presenters.Requests
{
    public class LoginRefreshTokenRequest
    {
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
