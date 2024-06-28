using Aplication.CQRS.Products.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;

namespace Aplication.CQRS.Products.Query.Request;

public class GetByBarcodeProductQueryRequest : IRequest<ResponseModel<GetByBarcodeProductQueryResponse>>
{
	public int Barcode { get; set; }

	public GetByBarcodeProductQueryRequest(int barcode)
	{
		Barcode = barcode;
	}
}
