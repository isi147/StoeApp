using Aplication.CQRS.Invoices.Command.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Invoices.Command.Request;

public class DeleteInvoiceCommandRequest : IRequest
{
	public int Id { get; set; }

	public DeleteInvoiceCommandRequest(int id)
	{
		Id = id;
	}
}
