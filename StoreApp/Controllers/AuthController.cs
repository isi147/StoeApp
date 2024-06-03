using A.StoreApp.Constants;
using Aplication.CQRS.Auth.Command.Request;
using Aplication.CQRS.Auth.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = $"{UserRoles.Admin}")]

public class AuthController : BaseController
{
	/// <summary>
	/// Login olmaq ucun endpoint
	/// </summary>
	/// <param name="userLogin"></param>
	/// <returns></returns>
	[HttpPost("Login")]
	[AllowAnonymous]
	[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Cashier},{UserRoles.Customer}")]
	public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest userLogin)
	{
		return Ok(await Sender.Send(userLogin));
	}

	[HttpPost("Registration")]
	[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Cashier},{UserRoles.Customer}")]

	public async Task<IActionResult> Registration([FromBody] RegistrationUserCommandRequest registration)
	{
		return Ok(await Sender.Send(registration));
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateUserCommandRequest request)
	{
		await Sender.Send(request);
		return Ok();

	}

	[HttpDelete("{id}")]
	[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Cashier}")]
	public async Task<IActionResult> Delete(int id)
	{
		var request = new DeleteUserCommandRequest(id);
		await Sender.Send(request);
		return Ok();
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
