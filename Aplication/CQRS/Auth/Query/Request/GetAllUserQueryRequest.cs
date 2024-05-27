using Aplication.CQRS.Auth.Query.Response;
using Aplication.CQRS.Categories.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Auth.Query.Request;

public class GetAllUserQueryRequest : IRequest<ResponseModelPagination<GetAllUserQueryResponse>>
{
	public int Limit { get; set; } = 10;
	public int Page { get; set; } = 1;

}
