using Aplication.CQRS.Categories.Query.Request;
using Aplication.CQRS.Categories.Query.Response;
using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Categories.Handler.QueryHandlers;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, ResponseModelPagination<GetAllCategoryQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModelPagination<GetAllCategoryQueryResponse>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
	{
		var categories = _unitOfWork.CategoryRepository.GetAll();
		var totalCount = categories.Count();
		if (!categories.Any())
		{
			return new ResponseModelPagination<GetAllCategoryQueryResponse>
			{
				Data = new Pagination<GetAllCategoryQueryResponse>
				{
					Data = { },
					TotalDataCount = totalCount,
				},

				Errors = []
			};
		}
		categories = categories.Skip((request.Page - 1) * request.Limit).Take(request.Limit);
		var list = new List<GetAllCategoryQueryResponse>();
		foreach (var category in categories)
		{
			var mappedCategory = new GetAllCategoryQueryResponse
			{
				Name = category.Name,
				Id = category.Id

			};
			list.Add(mappedCategory);
		}
		return new ResponseModelPagination<GetAllCategoryQueryResponse>
		{
			Data = new Pagination<GetAllCategoryQueryResponse> { Data = list, TotalDataCount = totalCount }
		};
	}
}
