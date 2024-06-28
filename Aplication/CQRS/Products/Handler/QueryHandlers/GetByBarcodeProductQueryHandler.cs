using Aplication.CQRS.Products.Query.Request;
using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Products.Handler.QueryHandlers;

public class GetByBarcodeProductQueryHandler : IRequestHandler<GetByBarcodeProductQueryRequest, ResponseModel<GetByBarcodeProductQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetByBarcodeProductQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<GetByBarcodeProductQueryResponse>> Handle(GetByBarcodeProductQueryRequest request, CancellationToken cancellationToken)
	{
		var currentProduct = await _unitOfWork.ProductRepository.GetByBarcodeAync(request.Barcode);
		var mappedProduct = new GetByBarcodeProductQueryResponse
		{
			Id = currentProduct.Id,
			Name = currentProduct.Name,
			Description = currentProduct.Description,
			Price = currentProduct.Price,
			Barcode = currentProduct.Barcode
		};

		return new ResponseModel<GetByBarcodeProductQueryResponse>(mappedProduct);
	}
}
