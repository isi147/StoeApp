using Aplication.CQRS.Products.Command.Request;
using Aplication.CQRS.Products.Command.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Products.Handler.CommandHandlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, ResponseModel<DeleteProductCommandResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<DeleteProductCommandResponse>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
	{
		_unitOfWork.ProductRepository.Delete(request.Id);
		await _unitOfWork.SaveChangesAsync();
		return new ResponseModel<DeleteProductCommandResponse>();
	}
}
