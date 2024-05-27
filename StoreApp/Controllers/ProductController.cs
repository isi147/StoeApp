using Aplication.CQRS.Products.Command.Request;
using Aplication.CQRS.Products.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Cashier")]

public class ProductController : BaseController
{
	[HttpPost]
	public async Task<IActionResult> AddAsync(CreateProductCommandRequest request)
	{
		return Ok(await Sender.Send(request));
	}

	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest request)
	{
		return Ok(await Sender.Send(request));
	}

	[HttpGet]
	[Route("getById")]
	public async Task<IActionResult> GetById(int id)
	{
		var request = new GetByIdProductQueryRequest(id);
		return Ok(await Sender.Send(request));
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var requst = new DeleteProductCommandRequest(id);
		return Ok(await Sender.Send(requst));
	}

	[HttpPut]
	public async Task<IActionResult>Update(UpdateProductCommandRequest request)
	{
		return Ok(await Sender.Send(request));
	}


}
