using Aplication.CQRS.Products.Query.Request;
using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Extensions;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Products.Handler.QueryHandlers;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, Pagination<GetAllProductQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllProductQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Pagination<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
	{

		var categories = _unitOfWork.ProductRepository.GetAll();
		var totalCount = categories.Count();
		//if (!categories.Any())
		//{
		//	return new Pagination<GetAllProductQueryResponse>
		//	{
		//		Data = new Pagination<GetAllProductQueryResponse>
		//		{
		//			Data = { },
		//			TotalDataCount = totalCount,
		//		},

		//		Errors = []
		//	};
		//}
		categories = categories.PageBy(request.Page, request.Limit);
		var list = new List<GetAllProductQueryResponse>();
		foreach (var category in categories)
		{
			var mappedCategory = new GetAllProductQueryResponse
			{
				Description = category.Description,
				Name = category.Name,
				Id = category.Id,
				CategoryId = category.CategoryId,
				Barcode = category.Barcode
			};
			list.Add(mappedCategory);
		}
		return new Pagination<GetAllProductQueryResponse>(list, totalCount, request.Page, request.Limit);

	}
}
