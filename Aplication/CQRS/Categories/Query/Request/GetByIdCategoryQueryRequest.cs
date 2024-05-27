using Aplication.CQRS.Categories.Query.Response;
using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Categories.Query.Request;

public class GetByIdCategoryQueryRequest:IRequest<ResponseModel<GetByIdCategoryQueryResponse>>
{
	public int Id { get; set; }

	public GetByIdCategoryQueryRequest(int id)
    {
        Id = id;
    }
}
