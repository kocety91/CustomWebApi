using System;

namespace CustomWebApi.Common
{
    public abstract class CustomExceptions : Exception
    {
        public CustomExceptions(string message)
            :base(message)
        {
        }
    }
}
