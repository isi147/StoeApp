namespace Aplication.DTOs.InvoiceItemDtos;

public class InvoiceItemRequestDto
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public int InvoiceId { get; set; }
}
