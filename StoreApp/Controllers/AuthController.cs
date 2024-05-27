using Aplication.CQRS.Auth.Command.Request;
using Aplication.CQRS.Auth.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]

public class AuthController : BaseController
{
	/// <summary>
	/// Login olmaq ucun endpoint
	/// </summary>
	/// <param name="userLogin"></param>
	/// <returns></returns>
	[HttpPost("Login")]
	[AllowAnonymous]

	public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest userLogin)
	{
		return Ok(await Sender.Send(userLogin));
	}

	[HttpPost("Registration")]


	public async Task<IActionResult> Registration([FromBody] RegistrationUserCommandRequest registration)
	{
		return Ok(await Sender.Send(registration));
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateUserCommandRequest request)
	{
		return Ok(await Sender.Send(request));

	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var request = new DeleteUserCommandRequest(id);
		return Ok(await Sender.Send(request));
	}


	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] GetAllUserQueryRequest request)
	{
		return Ok(await Sender.Send(request));
	}


	[HttpGet]
	[Route("getById/{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var request = new GetByIdUserQueryRequest(id);
		return Ok(await Sender.Send(request));
	}


}
