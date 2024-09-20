
using EShopping.Core.ViewModels;
using System.Net;

namespace EShopping.Core.Exceptions
{
    public class CustomException : CustomExceptionBase
    {
        public CustomException(string message, HttpStatusCode statusCode) : base(new CustomExceptionViewModel
        {
            Status = new StatusViewModel
            {
                Code = statusCode,
                Message = message
            }
        })
        { }

        public override CustomExceptionViewModel EmitResult()
        {
            return _exception;
        }
    }
}
