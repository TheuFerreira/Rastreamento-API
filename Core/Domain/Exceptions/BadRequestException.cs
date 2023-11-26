namespace Core.Domain.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() : base() { }
        public BadRequestException(string message) : base(message) { }
    }
}
