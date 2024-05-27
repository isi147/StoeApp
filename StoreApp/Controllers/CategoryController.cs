using Aplication.CQRS.Categories.Command.Request;
using Aplication.CQRS.Categories.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles ="Admin")]
public class CategoryController : BaseController
{
	[HttpPost]
	public async Task<IActionResult> AddAsync(CreateCategoryCommandRequest request)
	{
		return Ok(await Sender.Send(request));
	}

	[HttpGet]
	[Authorize(Roles = "Admin,Cashier")]

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
		return Ok(await Sender.Send(requst));
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateCategoryCommandRequest request)// id gotursun
	{
		return Ok(await Sender.Send(request));
	}


}
