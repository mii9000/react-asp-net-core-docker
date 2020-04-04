using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SPA.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {

        }

    }
}
