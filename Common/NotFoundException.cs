namespace CustomWebApi.Common
{
    public class NotFoundException : CustomExceptions
    {
        public NotFoundException(string message) 
            : base(message)
        {
        }
    }
}
