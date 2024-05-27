using Domain.Entities;
using Domain.Enums;

namespace Aplication.CQRS.Invoices.Query.Response;

public class GetByIdInvoiceQueryResponse
{
	public int Id { get; set; }
	public int CashierId { get; set; }
	public int CustomerId { get; set; }
	public int Barcode { get; set; }
    public string InvoiceBarcode { get; set; }
    public InvoiceType InvoiceType { get; set; }
	public List<InvoiceItem> InvoiceItems { get; set; }
}
