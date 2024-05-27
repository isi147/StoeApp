using Aplication.CQRS.Auth.Query.Request;
using Aplication.CQRS.Auth.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Auth.Handler.QueryHandler;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, ResponseModelPagination<GetAllUserQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllUserQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}


	public async Task<ResponseModelPagination<GetAllUserQueryResponse>> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
	{
		var categories = _unitOfWork.UserRepository.GetAll();
		var totalCount = categories.Count();
		if (!categories.Any())
		{
			return new ResponseModelPagination<GetAllUserQueryResponse>
			{
				Data = new Pagination<GetAllUserQueryResponse>
				{
					Data = { },
					TotalDataCount = totalCount,
				},
				Errors = []
			};
		}
		categories = categories.Skip((request.Page - 1) * request.Limit).Take(request.Limit);
		var list = new List<GetAllUserQueryResponse>();
		foreach (var category in categories)
		{
			var mappedCategory = new GetAllUserQueryResponse
			{
				Name = category.Name,
				Surname = category.Surname,
				Email = category.Email,
				Id = category.Id,
				UserType = category.UserType
			};
			list.Add(mappedCategory);
		}
		return new ResponseModelPagination<GetAllUserQueryResponse>
		{
			Data = new Pagination<GetAllUserQueryResponse> { Data = list, TotalDataCount = totalCount }
		};
	}
}
