using Aplication.CQRS.Users.Command.Request;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Users.Handler.CommandHandler;

internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
	{
		_unitOfWork.UserRepository.Delete(request.Id);
		await _unitOfWork.SaveChangesAsync();
	}
}
