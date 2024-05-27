using Domain.Entity;

namespace Aplication.CQRS.Products.Command.Response;

public class CreateProductCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

}
