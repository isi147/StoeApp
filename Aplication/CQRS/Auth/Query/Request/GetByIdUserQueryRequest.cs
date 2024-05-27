using Aplication.CQRS.Auth.Query.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Auth.Query.Request;

public class GetByIdUserQueryRequest : IRequest<ResponseModel<GetByIdUseryQueryResponse>>
{
	public int Id { get; set; }

	public GetByIdUserQueryRequest(int id)
	{
		Id = id;
	}
}
