using InSynq.Core.ResponseWrapper;
using Microsoft.AspNetCore.Mvc;

namespace InSynq.Web.Api.Controllers._Base;

[Route("api/[controller]/[action]")]
[ApiController]
public class BaseController : ControllerBase
{
	public IActionResult Result(ResponseWrapper response) => response.IsSuccess ? Ok() : BadRequest(response.Error);

	public IActionResult Result(ResponseWrapper response, string message) => response.IsSuccess ? Ok(message) : BadRequest(response.Error);

	public IActionResult Result<T>(ResponseWrapper<T> response) => response.IsSuccess ? Ok(response.Data) : BadRequest(response.Error);

	public IActionResult ResultCreated(ResponseWrapper response) => response.IsSuccess ? Created() : BadRequest(response.Error);

	public IActionResult ResultNoContent(ResponseWrapper response) => response.IsSuccess ? NoContent() : BadRequest(response.Error);

	public IActionResult ResultNotFound(ResponseWrapper response) => response.IsSuccess ? Ok() : NotFound(response.Error);

	public IActionResult ResultNotFound<T>(ResponseWrapper<T> response) => response.IsSuccess ? Ok(response.Data) : NotFound(response.Error);
}