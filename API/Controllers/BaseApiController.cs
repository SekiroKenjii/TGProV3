using Core.Constants;
using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(T result, string? action = null)
        {
            if (!result!.Equals(default))
            {
                var response = new Response<T>
                {
                    Errors = default,
                    Data = result
                };

                switch (action)
                {
                    case Applications.Actions.Add:
                        response.StatusCode = (int)HttpStatusCode.Created;
                        response.Message = Messages.ADD_SUCCESS;
                        break;
                    case Applications.Actions.Update:
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = Messages.UPDATE_SUCCESS;
                        break;
                    case Applications.Actions.Delete:
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = Messages.DELETE_SUCCESS;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = HttpStatusCode.OK.ToString();
                        break;
                }

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
