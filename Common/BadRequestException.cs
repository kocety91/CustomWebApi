namespace CustomWebApi.Common
{
    public class BadRequestException : CustomExceptions
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
