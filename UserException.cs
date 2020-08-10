using System;

namespace BotTemplate
{
    public class UserException : Exception
    {
        public UserException()
        {
        }

        public UserException(string message) : base(message)
        {
        }
    }
}
