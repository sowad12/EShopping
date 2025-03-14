using System.Net;

namespace EShopping.Core.ViewModels
{

    //public class ValidationErrorResponseModel
    public class ValidationErrorResponseViewModel
    {

        public static class ErrorType
        {
            public static string Input = "input";
            public static string Domain = "domain";
        }

        public string Type { get; set; }
        public HttpStatusCode Code { get; set; }
        public object Source { get; set; }
        public string Message { get; set; }


        public ValidationErrorResponseViewModel()
        {
        }

        public ValidationErrorResponseViewModel(string type, HttpStatusCode code, object source, string message)
        {
            Type = type;
            Code = code;
            Source = source;
            Message = message;
        }

    }
}
