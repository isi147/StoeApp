using Aplication.CQRS.Invoices.Query.Reqeust;
using Aplication.CQRS.Invoices.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Invoices.Handler.QueryHandlars;

public class GetByIdInvoiceQueryHandler : IRequestHandler<GetByIdInvoiceQueryRequest, ResponseModel<GetByIdInvoiceQueryResponse>>
{

	private readonly IUnitOfWork _unitOfWork;

	public GetByIdInvoiceQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<GetByIdInvoiceQueryResponse>> Handle(GetByIdInvoiceQueryRequest request, CancellationToken cancellationToken)
	{
		var currentInvoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(request.Id);
		var mappedInvoice = new GetByIdInvoiceQueryResponse
		{
			Id = currentInvoice.Id,
			CashierId = currentInvoice.CashierId,
			CustomerId = currentInvoice.CustomerId,
			Barcode = currentInvoice.Barcode,
			InvoiceType = currentInvoice.InvoiceType,
			InvoiceItems = currentInvoice.InvoiceItems.Select(item => new InvoiceItem
			{
				Id = item.Id,
				InvoiceId = item.InvoiceId,
				ProductId = item.ProductId,
				Quantity = item.Quantity,
			}).ToList()

		};
		return new ResponseModel<GetByIdInvoiceQueryResponse>(mappedInvoice);

	}
}
