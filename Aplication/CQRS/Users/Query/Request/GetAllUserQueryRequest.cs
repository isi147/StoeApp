using Aplication.CQRS.Users.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Users.Query.Request;

public class GetAllUserQueryRequest : IRequest<Pagination<GetAllUserQueryResponse>>
{
	public int Limit { get; set; } = 15;
	public int Page { get; set; } = 1;
}
