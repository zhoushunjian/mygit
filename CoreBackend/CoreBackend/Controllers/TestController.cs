using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBackend.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private MyContext _context;

        public TestController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}