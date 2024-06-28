using Aplication.CQRS.Users.Query.Request;
using Aplication.CQRS.Users.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Users.Handler.QueryHandler;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQueryRequest, ResponseModel<GetByIdUserQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetByIdUserQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<GetByIdUserQueryResponse>> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
	{
		var curentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
		var mappedUser = new GetByIdUserQueryResponse
		{
			Id = curentUser.Id,
			Name = curentUser.Name,
			Surname = curentUser.Surname,
			Email = curentUser.Email
		};
		return new ResponseModel<GetByIdUserQueryResponse>(mappedUser);
	}
}
