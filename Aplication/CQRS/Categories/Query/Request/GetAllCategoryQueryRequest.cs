using Aplication.CQRS.Categories.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Categories.Query.Request;

public class GetAllCategoryQueryRequest:IRequest<Pagination<GetAllCategoryQueryResponse>>
{
    public int Limit { get; set; } = 10;
    public int Page { get; set; } = 1; 
}
