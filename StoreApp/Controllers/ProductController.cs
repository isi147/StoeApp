using A.StoreApp.Constants;
using Aplication.CQRS.Products.Command.Request;
using Aplication.CQRS.Products.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]

[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Cashier}")]

public class ProductController : BaseController
{
	[HttpPost]

	public async Task<IActionResult> AddAsync(CreateProductCommandRequest request)
	{
		return Ok(await Sender.Send(request));
	}

	[HttpGet]
	[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Cashier},{UserRoles.Customer}")]
	public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest request)
	{
		return Ok(await Sender.Send(request));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var request = new GetByIdProductQueryRequest(id);
		return Ok(await Sender.Send(request));
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var requst = new DeleteProductCommandRequest(id);
		await Sender.Send(requst);
		return Ok();
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateProductCommandRequest request)
	{
		await Sender.Send(request);
		return Ok();
	}


}
