namespace Core.Domain.Exceptions
{
    public class BadRequestException : BaseException
    {
        private readonly string message;

        public BadRequestException(string message)
        {
            this.message = message ?? string.Empty;
        }
    }
}
