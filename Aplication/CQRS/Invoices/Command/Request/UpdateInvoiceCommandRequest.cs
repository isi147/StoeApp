using Aplication.CQRS.Invoices.Command.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Aplication.CQRS.Invoices.Command.Request;

public class UpdateInvoiceCommandRequest : IRequest
{
	public int Id { get; set; }
	public int CashierId { get; set; }
	public int CustomerId { get; set; }
	public int Barcode { get; set; }
	public InvoiceType InvoiceType { get; set; }
	public List<InvoiceItem> InvoiceItems { get; set; }
}
