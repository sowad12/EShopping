
using EShopping.Core.ViewModels;
using System;

namespace EShopping.Core.Exceptions
{
    public abstract class CustomExceptionBase : Exception
    {
        public CustomExceptionViewModel _exception = null;
        public CustomExceptionBase(CustomExceptionViewModel exception) : base(exception.Status.Message)
        {
            _exception = exception;
        }

        public abstract CustomExceptionViewModel EmitResult();

    }
}
