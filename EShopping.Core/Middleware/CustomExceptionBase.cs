using EShopping.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Core.Middleware
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
