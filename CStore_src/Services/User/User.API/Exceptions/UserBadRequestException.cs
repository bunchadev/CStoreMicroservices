using CommonLib.Exceptions;

namespace User.API.Exceptions
{
    public class UserBadRequestException : BadRequestException
    {
        public UserBadRequestException(string message) : base("User",message)
        {
            
        }
    }
}
