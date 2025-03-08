//using CB.Core.Interfaces.Exceptions;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Threading.Tasks;

//namespace CB.Core.Exceptions
//{
//    public class ExceptionHandler : IExceptionHandler
//    {
//        private readonly IExceptionResponseWriter _exceptionResponseWriter;

//        private static readonly IDictionary<Type, ExceptionClientResponse> ExceptionTypeCache = new Dictionary<Type, ExceptionClientResponse>
//        {
//            {typeof(ArgumentOutOfRangeException), new ExceptionClientResponse("Requested parameter out of range.", HttpStatusCode.BadRequest)},
//            {typeof(AuthorizationFailureException), new ExceptionClientResponse("Not Authorized", HttpStatusCode.Unauthorized) }
//        };

//        public ExceptionHandler(IExceptionResponseWriter exceptionResponseWriter)
//        {
//            _exceptionResponseWriter = exceptionResponseWriter;
//        }

//        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
//        {
//            if (ex == null) return;
//            var code = HttpStatusCode.InternalServerError;
//            var clientMessage = "Internal server error";

//            //var message = ex.InnerException == null ? $"EXCEPTION ==> {ex.Message}" :
//            //    "EXCEPTION ==>"+ ("(1). " + ex.Message + (" (2). " + ex.InnerException.Message)) +
//            //    (ex.InnerException?.InnerException == null ? "" : " (3). " + ex.InnerException.InnerException.Message);

//            // Check for specific exeption types here and
//            // set "code" to appropriate HttpStatusCode
//            if (ExceptionTypeCache.ContainsKey(ex.GetType()))
//            {
//                var exceptionClientResponse = ExceptionTypeCache[ex.GetType()];
//                code = exceptionClientResponse.StatusCode;
//                clientMessage = exceptionClientResponse.ClientMessage;
//            }

//            await _exceptionResponseWriter.WriteExceptionAsync(context, ex, code, clientMessage);
//        }

//        private class ExceptionClientResponse
//        {
//            public ExceptionClientResponse(string clientMessage, HttpStatusCode statusCode)
//            {
//                ClientMessage = clientMessage;
//                StatusCode = statusCode;
//            }

//            public string ClientMessage { get; }
//            public HttpStatusCode StatusCode { get; }
//        }
//    }

//}
