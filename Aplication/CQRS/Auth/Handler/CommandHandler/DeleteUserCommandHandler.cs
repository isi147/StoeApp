using Aplication.CQRS.Auth.Command.Request;
using Aplication.CQRS.Auth.Command.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Auth.Handler.CommandHandler;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, ResponseModel<DeleteUserCommandResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<DeleteUserCommandResponse>> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
	{
		_unitOfWork.UserRepository.Delete(request.Id);
		await _unitOfWork.SaveChangesAsync();
		return new ResponseModel<DeleteUserCommandResponse> ();
	}
}
