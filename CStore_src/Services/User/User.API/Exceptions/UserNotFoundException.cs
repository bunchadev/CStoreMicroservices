using CommonLib.Exceptions;
namespace User.API.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid Id) : base("User", Id)
        {
        }
    }
}
