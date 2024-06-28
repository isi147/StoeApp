using MediatR;

namespace Aplication.CQRS.Users.Command.Request;

public record DeleteUserCommandRequest(int Id) : IRequest;
