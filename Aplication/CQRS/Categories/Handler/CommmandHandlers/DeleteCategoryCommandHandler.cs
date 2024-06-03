using Aplication.CQRS.Categories.Command.Request;
using Aplication.CQRS.Categories.Command.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Categories.Handler.CommmandHandlers;

public class DeleteCategoryCommandHandler:IRequestHandler<DeleteCategoryCommandRequest>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
	{
		_unitOfWork.CategoryRepository.Delete(request.Id);
		await _unitOfWork.SaveChangesAsync();

	}
}
