using Aplication.CQRS.Products.Command.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Products.Command.Request;

public class DeleteProductCommandRequest:IRequest<ResponseModel<DeleteProductCommandResponse>>
{
    public int Id { get; set; }

	public DeleteProductCommandRequest(int id)
	{
		Id = id;
	}
}
