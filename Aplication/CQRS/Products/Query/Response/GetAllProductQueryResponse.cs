using Domain.Entity;

namespace Aplication.CQRS.Products.Query.Response;

public class GetAllProductQueryResponse
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public int CategoryId { get; set; }
	public int Barcode { get; set; }


}
