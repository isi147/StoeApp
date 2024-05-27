using Aplication.CQRS.Auth.Command.Response;
using Aplication.CQRS.Categories.Command.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Auth.Command.Request;

public class UpdateUserCommandRequest : IRequest<ResponseModel<UpdateUserCommandResponse>>
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }


}
