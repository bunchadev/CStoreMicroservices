using CommonLib.Exceptions;

namespace Catalog.API.Exceptions
{
    public class ProductBadRequestException : BadRequestException
    {
        public ProductBadRequestException(string message) : base("Product",message)
        {
        }
    }
}
