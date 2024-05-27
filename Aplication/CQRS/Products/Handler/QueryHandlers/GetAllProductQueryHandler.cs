using Aplication.CQRS.Products.Query.Request;
using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Products.Handler.QueryHandlers;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, ResponseModelPagination<GetAllProductQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllProductQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModelPagination<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
	{

		var categories = _unitOfWork.ProductRepository.GetAll();
		var totalCount = categories.Count();
		if (!categories.Any())
		{
			return new ResponseModelPagination<GetAllProductQueryResponse>
			{
				Data = new Pagination<GetAllProductQueryResponse>
				{
					Data = { },
					TotalDataCount = totalCount,
				},

				Errors = []
			};
		}
		categories = categories.Skip((request.Page - 1) * request.Limit).Take(request.Limit);
		var list = new List<GetAllProductQueryResponse>();
		foreach (var category in categories)
		{
			var mappedCategory = new GetAllProductQueryResponse
			{
				Description = category.Description,
				Name = category.Name,
				Id = category.Id,
				CategoryId = category.CategoryId,


			};
			list.Add(mappedCategory);
		}
		return new ResponseModelPagination<GetAllProductQueryResponse>
		{
			Data = new Pagination<GetAllProductQueryResponse> { Data = list, TotalDataCount = totalCount }
		};
	}
}
