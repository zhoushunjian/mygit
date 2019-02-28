using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoreBackend.Dtos
{
    public class BaseResponse
    {
        public bool Success { get; set; }

        public String Message { get; set; }

        public int StatusCode { get; set; }

        public BaseResponse()
        {
            Success = true;
            StatusCode = HttpStatusCode.OK.GetHashCode();
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public BaseResponse()
        {
            Success = true;
            StatusCode = HttpStatusCode.OK.GetHashCode();
        }
    }
}
