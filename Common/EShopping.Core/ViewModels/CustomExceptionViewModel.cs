using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Core.ViewModels
{
    public class CustomExceptionViewModel : FailResponseViewModel
    {
        public CustomExceptionViewModel(string message = "error", HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            Status = new StatusViewModel
            {
                Code = code,
                Message = message
            };
        }

    }
}
