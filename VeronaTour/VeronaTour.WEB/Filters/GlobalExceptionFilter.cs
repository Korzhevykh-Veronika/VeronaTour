using NLog;
using System.Web.Mvc;

namespace VeronaTour.WEB.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext сontext)
        {
            logger.Fatal(сontext.Exception, "Unhandled excepition");
        }
    }
}