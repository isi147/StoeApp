using Aplication.CQRS.Invoices.Command.Request;
using Aplication.CQRS.Invoices.Query.Reqeust;
using Aplication.CQRS.Products.Command.Request;
using Aplication.CQRS.Products.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A.StoreApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Cashier")]


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
		return Ok(await Sender.Send(requst));
	}

	[HttpPut]
	public async Task<IActionResult> Update(UpdateInvoiceCommandRequest request)
	{
		return Ok(await Sender.Send(request));
	}
	[HttpGet]
	[Route("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var request = new GetByIdInvoiceQueryRequest(id);
		return Ok(await Sender.Send(request));
	}

	[HttpPost]
	[Route ("refund")]
	public async Task<IActionResult> CreateRefundInvoice(CreateRefundInvoiceCommandRequest request)
	{
		return Ok(await Sender.Send(request));
	}

}
