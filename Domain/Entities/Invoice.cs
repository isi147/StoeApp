using Domain.BaseEntities;
using Domain.Enums;

namespace Domain.Entities;

public class Invoice : BaseEntity
{
	public string ?InvoiceBarcode { get; set; }
	public int CashierId { get; set; }
	public int CustomerId { get; set; }
	public int Barcode { get; set; }
    public int? SellInvoiceId { get; set; }
    public InvoiceType InvoiceType { get; set; }
	public IEnumerable<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
