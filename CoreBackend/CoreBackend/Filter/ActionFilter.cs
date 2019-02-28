using CoreBackend.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoreBackend.Filter
{
    /// <summary>
    /// 方法拦截器
    /// </summary>
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //跳过全局过滤
            if (context.Filters.Any(s => s.GetType() == typeof(SkipActionFilter)))
            {
                return;
            }

            if (context.Result != null && context.Result.GetType() != typeof(EmptyResult))
            {
                //获取数据
                var data = ((Microsoft.AspNetCore.Mvc.ObjectResult)context.Result).Value;
                if (data == null)
                {
                    context.Result = new JsonResult(new BaseResponse());
                    return;
                }
                //获取泛型类型
                var resultType = typeof(BaseResponse<>).MakeGenericType(new Type[] { data.GetType() });
                //实例泛型类型
                var result = Activator.CreateInstance(resultType);
                //获取属性
                var properties = resultType.GetProperties();
                //构造返参
                foreach (var property in properties)
                {
                    if (property.Name == "Data" && context.Result != null)
                    {
                        property.SetValue(result, data);
                    }
                }
                context.Result = new JsonResult(result);
            }
            else
            {
                context.Result = new JsonResult(new BaseResponse());
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorMsg = string.Empty;
                var isFirst = true;

                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        var msg = !string.IsNullOrEmpty(error.ErrorMessage) ? error.ErrorMessage : error.Exception.Message;
                        errorMsg += isFirst ? msg : "|" + msg;
                        isFirst = false;
                    }
                }

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    context.Result = new JsonResult(new BaseResponse() { Message = errorMsg, StatusCode = HttpStatusCode.BadRequest.GetHashCode(), Success = false });
                }
            }
        }
    }
}
