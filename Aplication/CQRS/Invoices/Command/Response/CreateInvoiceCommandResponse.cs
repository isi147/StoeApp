using Aplication.DTOs.InvoiceItemDtos;
using Domain.Enums;

namespace Aplication.CQRS.Invoices.Command.Response;

public class CreateInvoiceCommandResponse
{
	public int Id { get; set; }
	public int CashierId { get; set; }
	public int CustomerId { get; set; }
	public int Barcode { get; set; }
	public string InvoiceBarcode { get; set; }
	public InvoiceType InvoiceType { get; set; }
	public List<InvoiceItemRequestDto> InvoiceItems { get; set; } = new List<InvoiceItemRequestDto>();
}
