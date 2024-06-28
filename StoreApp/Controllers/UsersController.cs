using A.StoreApp.Constants;
using Aplication.CQRS.Users.Command.Request;
using Aplication.CQRS.Users.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : BaseController
	{
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
}
