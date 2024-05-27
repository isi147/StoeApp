using Aplication.CQRS.Products.Command.Response;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Entity;
using MediatR;

namespace Aplication.CQRS.Products.Command.Request;

public class UpdateProductCommandRequest : IRequest<ResponseModel<UpdateProductCommandResponse>>
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int CategoryId { get; set; }

}
