using Aplication.CQRS.Invoices.Query.Reqeust;
using Aplication.CQRS.Invoices.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Invoices.Handler.QueryHandlars;

public class GetAllInvoiceQueryHandler : IRequestHandler<GetAllInvoiceQueryRequest, ResponseModel<GetAllInvoiceQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllInvoiceQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<GetAllInvoiceQueryResponse>> Handle(GetAllInvoiceQueryRequest request, CancellationToken cancellationToken)
	{
		var categories = _unitOfWork.InvoiceRepository.GetAll();
		var totalCount = categories.Count();
		if (!categories.Any())
		{
			return new ResponseModelPagination<GetAllInvoiceQueryResponse>
			{
				Data = new Pagination<GetAllInvoiceQueryResponse>
				{
					Data = { },
					TotalDataCount = totalCount
				},
				Errors = []
			};
		}
		categories = categories.Skip((request.Page - 1) * request.Limit).Take(request.Limit);
		var list = new List<GetAllInvoiceQueryResponse>();
		foreach (var category in categories)
		{
			var currentInvoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(category.Id);
			var mappedCategory = new GetAllInvoiceQueryResponse
			{
				CashierId = category.CashierId,
				CustomerId = category.CustomerId,
				Id = category.Id,
				InvoiceType = category.InvoiceType,
				Barcode = category.Barcode,
				InvoiceBarcode = category.InvoiceBarcode!,
				
				InvoiceItems = currentInvoice.InvoiceItems.Select(item => new InvoiceItem
				{
					Id = item.Id,
					InvoiceId = item.InvoiceId,
					ProductId = item.ProductId,
					Quantity = item.Quantity
				}).ToList()

			};
			list.Add(mappedCategory);
		}
		return new ResponseModelPagination<GetAllInvoiceQueryResponse>
		{
			Data = new Pagination<GetAllInvoiceQueryResponse>
			{
				Data = list,
				TotalDataCount = totalCount
			}
		};
	}
}
