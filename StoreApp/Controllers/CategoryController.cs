using A.StoreApp.Constants;
using Aplication.CQRS.Categories.Command.Request;
using Aplication.CQRS.Categories.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Cashier}")]
public class CategoryController : BaseController
{
	[HttpPost]
	public async Task<IActionResult> AddAsync(CreateCategoryCommandRequest request)
	{
		return Ok(await Sender.Send(request));
	}

	[HttpGet]
	[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Cashier}")]

	public async Task<IActionResult> GetAll([FromQuery] GetAllCategoryQueryRequest request)
	{
		return Ok(await Sender.Send(request));
	}
	[HttpGet]
	[Route("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var request = new GetByIdCategoryQueryRequest(id);
		return Ok(await Sender.Send(request));
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var requst = new DeleteCategoryCommandRequest(id);
		await Sender.Send(requst);
		return Ok();
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateCategoryCommandRequest request)// id gotursun
	{
		await Sender.Send(request);
		return Ok();
	}


}
