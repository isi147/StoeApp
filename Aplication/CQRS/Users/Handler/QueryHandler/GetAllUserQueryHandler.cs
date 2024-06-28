using Aplication.CQRS.Users.Query.Request;
using Aplication.CQRS.Users.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Extensions;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Users.Handler.QueryHandler;

public class GetAllUserQueryHandler: IRequestHandler<GetAllUserQueryRequest, Pagination<GetAllUserQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllUserQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}


	public async Task<Pagination<GetAllUserQueryResponse>> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
	{
		var categories = _unitOfWork.UserRepository.GetAll();
		var totalCount = categories.Count();
		//if (!categories.Any())
		//{
		//	return new Pagination<GetAllUserQueryResponse>(new Pagination<GetAllUserQueryResponse>(new List<GetAllUserQueryResponse>(), totalCount, request.Page, request.Limit));
		//}

		categories = categories.PageBy(request.Page, request.Limit);
		var list = categories.Select(c => new GetAllUserQueryResponse() { Id = c.Id, Name = c.Name, Surname = c.Surname, Email = c.Email, UserType = c.UserType }).ToList();

		return new Pagination<GetAllUserQueryResponse>(list, totalCount, request.Page, request.Limit);

	}
}
