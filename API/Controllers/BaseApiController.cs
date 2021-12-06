using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(T result)
        {
            if (!result!.Equals(default))
            {
                return Ok(new Response<T>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = HttpStatusCode.OK.ToString(),
                    Errors = default,
                    Data = result
                });
            }

            return BadRequest(new Response<T>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = HttpStatusCode.BadRequest.ToString(),
                Errors = default,
                Data = default
            });
        }
    }
}
