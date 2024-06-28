using Aplication.CQRS.Users.Command.Request;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Users.Handler.CommandHandler;

internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
	{
		var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);

		currentUser.Name = request.Name;
		currentUser.Surname = request.Surname;
		currentUser.Email = request.Email;
		_unitOfWork.UserRepository.Update(currentUser);
		await _unitOfWork.SaveChangesAsync();

	}
}
