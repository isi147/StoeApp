using Aplication.CQRS.Categories.Query.Request;
using Aplication.CQRS.Categories.Query.Response;
using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Extensions;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Categories.Handler.QueryHandlers;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, Pagination<GetAllCategoryQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Pagination<GetAllCategoryQueryResponse>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
	{
		var categories = _unitOfWork.CategoryRepository.GetAll();
		var totalCount = categories.Count();
		//if (!categories.Any())
		//{
		//return new Pagination<GetAllCategoryQueryResponse>
		//{
		//	Data = new GetAllCategoryQueryResponse() { }

		//};
		//}
		categories = categories.PageBy(request.Page, request.Limit);
		var list = categories.Select(c => new GetAllCategoryQueryResponse() { Id = c.Id, Name = c.Name }).ToList();
		//foreach (var category in categories) //// BU kod 33 cu setrdeki kodla evez olundu deyisek inkisaf ededk )
		//{
		//	var mappedCategory = new GetAllCategoryQueryResponse
		//	{
		//		Name = category.Name,
		//		Id = category.Id

		//	};
		//	list.Add(mappedCategory);	
		//}
		return new Pagination<GetAllCategoryQueryResponse>(list, totalCount, request.Page, request.Limit);

	}
}
