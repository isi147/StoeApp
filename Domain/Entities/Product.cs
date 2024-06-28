using Domain.BaseEntities;

namespace Domain.Entity;

public class Product : BaseEntity
{
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int CategoryId { get; set; }
	public int Barcode { get; set; }
}
