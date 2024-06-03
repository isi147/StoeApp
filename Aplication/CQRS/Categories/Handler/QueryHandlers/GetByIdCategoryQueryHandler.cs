using Aplication.CQRS.Categories.Query.Request;
using Aplication.CQRS.Categories.Query.Response;
using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Categories.Handler.QueryHandlers;

public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQueryRequest, ResponseModel<GetByIdCategoryQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetByIdCategoryQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<GetByIdCategoryQueryResponse>> Handle(GetByIdCategoryQueryRequest request, CancellationToken cancellationToken)
	{
		var currentCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
		var mappedCategory = new GetByIdCategoryQueryResponse
		{
			Id = currentCategory.Id,
			Name = currentCategory.Name,

		};

		return new ResponseModel<GetByIdCategoryQueryResponse>(mappedCategory);
	}
}
