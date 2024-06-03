using MediatR;

namespace Aplication.CQRS.Auth.Command.Request;

public record DeleteUserCommandRequest(int Id) : IRequest;

