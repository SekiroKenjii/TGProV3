using Core.Constants;
using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/internal/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(T result, string? action = null)
        {
            if (!result!.Equals(default))
            {
                var response = new Response<T>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Errors = default,
                    Data = result
                };

                response.Message = action switch
                {
                    Applications.Actions.Add => Messages.ADD_SUCCESS,
                    Applications.Actions.Update => Messages.UPDATE_SUCCESS,
                    Applications.Actions.Delete => Messages.DELETE_SUCCESS,
                    _ => HttpStatusCode.OK.ToString()
                };

                return Ok(response);
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
