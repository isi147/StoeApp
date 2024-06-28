using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Products.Query.Request;

public class GetAllProductQueryRequest:IRequest<Pagination<GetAllProductQueryResponse>>
{
    public int Limit { get; set; } = 15;
    public int Page { get; set; } = 1;
}
