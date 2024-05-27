namespace Domain.Entities;

public class InvoiceItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
	public decimal Quantity { get; set; }
	public int InvoiceId { get; set; }
}
