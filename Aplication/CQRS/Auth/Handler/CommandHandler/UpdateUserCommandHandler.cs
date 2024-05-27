using Aplication.CQRS.Auth.Command.Request;
using Aplication.CQRS.Auth.Command.Response;
using Common.Exceptions;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Auth.Handler.CommandHandler;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, ResponseModel<UpdateUserCommandResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<UpdateUserCommandResponse>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
	{
		var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
		if (currentUser == null)
		{
			throw new BadRequestException();
		}
		currentUser.Name = request.Name;
		currentUser.Surname = request.Surname;
		currentUser.Email = request.Email;
		_unitOfWork.UserRepository.Update(currentUser);
		await _unitOfWork.SaveChangesAsync();
		return new ResponseModel<UpdateUserCommandResponse> ();

	}
}
