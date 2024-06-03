using Aplication.CQRS.Categories.Command.Request;
using Aplication.CQRS.Categories.Command.Response;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Entity;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Categories.Handler.CommmandHandlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, ResponseModel<CreateCategoryCommandResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<CreateCategoryCommandResponse>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
	{
		var newCategory = new Category
		{
			Name = request.Name,
			CreatedDate = DateTime.Now
		};
		await _unitOfWork.CategoryRepository.AddAsync(newCategory);
		await _unitOfWork.SaveChangesAsync();

		var response = new CreateCategoryCommandResponse
		{
			Id = newCategory.Id,
			Name = newCategory.Name
		};
		return new ResponseModel<CreateCategoryCommandResponse>(response);

	}
}
