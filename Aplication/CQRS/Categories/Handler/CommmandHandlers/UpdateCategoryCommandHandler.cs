using Aplication.CQRS.Categories.Command.Request;
using Aplication.CQRS.Categories.Command.Response;
using Aplication.CQRS.Products.Command.Response;
using Common.Exceptions;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Categories.Handler.CommmandHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, ResponseModel<UpdateCategoryCommandResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<UpdateCategoryCommandResponse>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
	{
		var currentCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
		if (currentCategory == null)
		{
			throw new BadRequestException();

		}
		currentCategory.Name = request.Name;

		_unitOfWork.CategoryRepository.Update(currentCategory);
		await _unitOfWork.SaveChangesAsync();
		return new ResponseModel<UpdateCategoryCommandResponse> ();
	}
}

