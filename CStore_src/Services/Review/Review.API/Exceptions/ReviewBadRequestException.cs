using CommonLib.Exceptions;

namespace Review.API.Exceptions
{
    public class ReviewBadRequestException : BadRequestException
    {
        public ReviewBadRequestException(string message) : base("Review",message)
        {
        }
    }
}
