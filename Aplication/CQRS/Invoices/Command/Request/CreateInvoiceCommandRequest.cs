using Aplication.CQRS.Invoices.Command.Response;
using Aplication.DTOs.InvoiceItemDtos;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Invoices.Command.Request;

public class CreateInvoiceCommandRequest : IRequest<ResponseModel<CreateInvoiceCommandResponse>>
{
	public int CashierId { get; set; }
	public int CustomerId { get; set; }
    public List<InvoiceItemRequestDto> InvoiceItems { get; set; }

}
