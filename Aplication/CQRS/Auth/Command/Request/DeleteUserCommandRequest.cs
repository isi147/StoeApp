using Aplication.CQRS.Auth.Command.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Auth.Command.Request;

public class DeleteUserCommandRequest : IRequest<ResponseModel<DeleteUserCommandResponse>>
{
	public int Id { get; set; }
	public DeleteUserCommandRequest(int id)
	{
		Id = id;
	}

}
