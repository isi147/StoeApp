using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Products.Query.Request;

public class GetByIdProductQueryRequest : IRequest<ResponseModel<GetByIdProductQueryResponse>>
{
	public int Id { get; set; }
	public GetByIdProductQueryRequest(int id)
	{
		Id = id;
	}

}
