

//namespace EShopping.Core.Filters
//{
//    public class ExceptionFilter : IExceptionFilter
//    {

//        private readonly ILogger _logger;

//        public ExceptionFilter(ILoggerFactory loggerFactory)
//        {
//            _logger = loggerFactory.CreateLogger<ExceptionFilter>();
//        }

//        public void OnException(ExceptionContext context)
//        {
//            var message = context.Exception.Message;

//            if (context != null && context.Exception.GetType().IsSubclassOf(typeof(BusinessExceptionBase)))
//            {
//                context.Result = new ObjectResult((context.Exception as BusinessExceptionBase).EmitResult(message));
//            }
//            else
//            {
//                _logger.LogError(context.Exception.Message, context.Exception);
//                context.Result = new ObjectResult(new FailResponseViewModel
//                {
//                    Data = string.Empty,
//                    Status = new StatusViewModel
//                    {
//                        Message = message,
//                        Code = HttpStatusCode.InternalServerError
//                    },
//                    Exceptions = new string[] { context.Exception.GetType().Name }
//                });
//            }
//        }
//    }
//}
