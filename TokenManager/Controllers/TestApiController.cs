using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TokenManager.Filters;

namespace TokenManager.Controllers
{
    public class TestApiController : ApiController
    {
        [ApiTokenValidate]
        [HttpGet]
        public string Words()
        {
            return "this is a test word";
        }
    }
}
