using Aplication.CQRS.Auth.Query.Request;
using Aplication.CQRS.Auth.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Auth.Handler.QueryHandler;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQueryRequest, ResponseModel<GetByIdUseryQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetByIdUserQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<GetByIdUseryQueryResponse>> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
	{
		var curentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
		var mappedUser = new GetByIdUseryQueryResponse
		{
			Id = curentUser.Id,
			Name = curentUser.Name,
			Surname = curentUser.Surname,
			Email = curentUser.Email
		};
		return new ResponseModel<GetByIdUseryQueryResponse>(mappedUser);
	}
}
