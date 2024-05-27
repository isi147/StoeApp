using Aplication.CQRS.Auth.Command.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Enums;
using MediatR;

namespace Aplication.CQRS.Auth.Command.Request;

public class RegistrationUserCommandRequest : IRequest<ResponseModel<RegistrationUserCommandResponse>>
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public UserType UserType { get; set; }

}
