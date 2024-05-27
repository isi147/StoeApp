using Aplication.CQRS.Auth.Command.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Auth.Command.Request;
/// <summary>
/// login user command request
/// </summary>
public class LoginUserCommandRequest : IRequest<ResponseModel<LoginUserCommandResponse>>
{
	/// <summary>
	/// email of user
	/// </summary>
	public string Email { get; set; }
	/// <summary>
	/// password of user
	/// </summary>
	public string Password { get; set; }
}
