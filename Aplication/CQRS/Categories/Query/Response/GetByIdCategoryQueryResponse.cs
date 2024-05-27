using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Categories.Query.Response;

public class GetByIdCategoryQueryResponse
{
	public int Id { get; set; }
	public string Name { get; set; }

}
