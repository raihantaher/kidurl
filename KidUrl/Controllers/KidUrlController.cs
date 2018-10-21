using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KidUrl.Manager.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KidUrl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KidUrlController : ControllerBase
    {
        private readonly IKidUrlManager _kidUrlManager;

        public KidUrlController(IKidUrlManager kidUrlManager)
        {
            _kidUrlManager = kidUrlManager;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<string> ConvertUrl([FromBody] string url)
        {
            string result = _kidUrlManager.ConvertUrl(url);

            if(result == String.Empty)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}