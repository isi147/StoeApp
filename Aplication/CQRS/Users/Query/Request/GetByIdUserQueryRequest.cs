using Aplication.CQRS.Products.Query.Response;
using Aplication.CQRS.Users.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Users.Query.Request;

public class GetByIdUserQueryRequest: IRequest<ResponseModel<GetByIdUserQueryResponse>>
{
	public GetByIdUserQueryRequest(int id)
	{
		Id = id;
	}

	public int Id { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
}
