namespace Core
{
    public class JWTModel
    {
        public string Key { get; set; } = string.Empty;
        public int Expiration { get; set; }
    }
}
