using Aplication.CQRS.Products.Query.Request;
using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Products.Handler.QueryHandlers;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, ResponseModel<GetByIdProductQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetByIdProductQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<GetByIdProductQueryResponse>> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
	{
		var currentProduct = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
		var mappedProduct = new GetByIdProductQueryResponse 
		{
			Id = currentProduct.Id, 
			Name = currentProduct.Name,
			Description = currentProduct.Description,
			Price = currentProduct.Price 
		};

		return new ResponseModel<GetByIdProductQueryResponse>(mappedProduct);

	}
}

