using A.StoreApp.Constants;
using Aplication.CQRS.Invoices.Command.Request;
using Aplication.CQRS.Invoices.Query.Reqeust;
using Aplication.CQRS.Products.Command.Request;
using Aplication.CQRS.Products.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]

[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Cashier}")]


public class InvoiceController : BaseController
{
	[HttpPost]
	public async Task<IActionResult> AddAsync(CreateInvoiceCommandRequest request)
	{
		return Ok(await Sender.Send(request));
	}

	[HttpGet]
	public async Task<IActionResult> GetAll([FromQuery] GetAllInvoiceQueryRequest request)
	{
		return Ok(await Sender.Send(request));
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var requst = new DeleteInvoiceCommandRequest(id);
		await Sender.Send(requst);
		return Ok();
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateInvoiceCommandRequest request)
	{
		await Sender.Send(request);
		return Ok();
	}
	[HttpGet]
	[Route("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var request = new GetByIdInvoiceQueryRequest(id);
		return Ok(await Sender.Send(request));
	}

	[HttpPost]
	[Route("refund")]
	public async Task<IActionResult> CreateRefundInvoice(CreateRefundInvoiceCommandRequest request)
	{
		return Ok(await Sender.Send(request));
	}

}
