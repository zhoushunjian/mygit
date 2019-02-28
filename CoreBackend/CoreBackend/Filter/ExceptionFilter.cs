using CoreBackend.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace CoreBackend.Filter
{
    /// <summary>
    /// 异常拦截器
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        private ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                _logger.LogCritical("testException", context.Exception);
                var result = new BaseResponse() { StatusCode = HttpStatusCode.InternalServerError.GetHashCode(), Message = context.Exception.Message, Success = false };
                context.Result = new JsonResult(result);
            }
            context.ExceptionHandled = true;
        }
    }
}
