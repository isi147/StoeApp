using MediatR;

namespace Aplication.CQRS.Auth.Command.Request;

public class UpdateUserCommandRequest : IRequest
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }


}
