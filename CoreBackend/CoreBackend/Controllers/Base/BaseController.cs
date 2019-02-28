using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBackend.Dtos;
using CoreBackend.Filter;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BaseController : Controller
    {
        protected BaseResponse<T> ReturnSuccess<T>(T Data)
        {
            return new BaseResponse<T>() { Data = Data };
        }
    }
}