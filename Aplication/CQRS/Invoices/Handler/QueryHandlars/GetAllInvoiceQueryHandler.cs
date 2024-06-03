using Aplication.CQRS.Categories.Query.Response;
using Aplication.CQRS.Invoices.Query.Reqeust;
using Aplication.CQRS.Invoices.Query.Response;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Entities;
using Domain.Extensions;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Invoices.Handler.QueryHandlars;

public class GetAllInvoiceQueryHandler : IRequestHandler<GetAllInvoiceQueryRequest, Pagination<GetAllInvoiceQueryResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllInvoiceQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Pagination<GetAllInvoiceQueryResponse>> Handle(GetAllInvoiceQueryRequest request, CancellationToken cancellationToken)
	{
		var categories = _unitOfWork.InvoiceRepository.GetAll();
		var totalCount = categories.Count();
		//if (!categories.Any())
		//{
		//	return new Pagination<GetAllInvoiceQueryResponse>
		//	{
		//		Data = new Pagination<GetAllInvoiceQueryResponse>
		//		{
		//			Data = { },
		//			TotalDataCount = totalCount
		//		},
		//		Errors = []
		//	};
		//}
		categories = categories.PageBy(request.Page, request.Limit);

		var list = categories.Select(c => new GetAllInvoiceQueryResponse()
		{
			Id = c.Id,
			Barcode = c.Barcode,
			InvoiceBarcode = c.InvoiceBarcode,
			CashierId = c.CashierId,
			CustomerId = c.CustomerId,
			InvoiceType = c.InvoiceType,
			InvoiceItems = c.InvoiceItems.Select(z => new InvoiceItem()
			{
				Id = z.Id,
				InvoiceId = z.InvoiceId,
				ProductId = z.ProductId,
				Quantity = z.Quantity
			}).ToList()
		}).ToList();

		return new Pagination<GetAllInvoiceQueryResponse>(list, totalCount, request.Page, request.Limit);
	}

	//public async Task<ResponseModel<GetAllInvoiceQueryResponse>> Handle(GetAllInvoiceQueryRequest request, CancellationToken cancellationToken)
	//{
	//	var categories = _unitOfWork.InvoiceRepository.GetAll();
	//	var totalCount = categories.Count();
	//	//if (!categories.Any())
	//	//{
	//	//	return new Pagination<GetAllInvoiceQueryResponse>
	//	//	{
	//	//		Data = new Pagination<GetAllInvoiceQueryResponse>
	//	//		{
	//	//			Data = { },
	//	//			TotalDataCount = totalCount
	//	//		},
	//	//		Errors = []
	//	//	};
	//	//}
	//	categories = categories.PageBy(request.Page, request.Limit);


	//	var list = new List<GetAllInvoiceQueryResponse>();
	//	foreach (var category in categories)
	//	{
	//		var currentInvoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(category.Id);
	//		var mappedCategory = new GetAllInvoiceQueryResponse
	//		{
	//			CashierId = category.CashierId,
	//			CustomerId = category.CustomerId,
	//			Id = category.Id,
	//			InvoiceType = category.InvoiceType,
	//			Barcode = category.Barcode,
	//			InvoiceBarcode = category.InvoiceBarcode!,

	//			InvoiceItems = currentInvoice.InvoiceItems.Select(item => new InvoiceItem
	//			{
	//				Id = item.Id,
	//				InvoiceId = item.InvoiceId,
	//				ProductId = item.ProductId,
	//				Quantity = item.Quantity
	//			}).ToList()

	//		};
	//		list.Add(mappedCategory);
	//	}
	//	return new Pagination<GetAllInvoiceQueryResponse>(list, totalCount,request.Page,request.Limit);
	//}

	//Task<Pagination<GetAllInvoiceQueryResponse>> IRequestHandler<GetAllInvoiceQueryRequest, Pagination<GetAllInvoiceQueryResponse>>.Handle(GetAllInvoiceQueryRequest request, CancellationToken cancellationToken)
	//{
	//	var categories = _unitOfWork.InvoiceRepository.GetAll();
	//	var totalCount = categories.Count();
	//	//if (!categories.Any())
	//	//{
	//	//	return new Pagination<GetAllInvoiceQueryResponse>
	//	//	{
	//	//		Data = new Pagination<GetAllInvoiceQueryResponse>
	//	//		{
	//	//			Data = { },
	//	//			TotalDataCount = totalCount
	//	//		},
	//	//		Errors = []
	//	//	};
	//	//}
	//	categories = categories.PageBy(request.Page, request.Limit);

	//	var list = categories.Select(c => new GetAllInvoiceQueryResponse()
	//	{
	//		Id = c.Id,
	//		Barcode = c.Barcode,
	//		InvoiceBarcode = c.InvoiceBarcode,
	//		CashierId = c.CashierId,
	//		CustomerId = c.CustomerId,
	//		InvoiceType = c.InvoiceType,
	//		InvoiceItems = c.InvoiceItems.Select(z => new InvoiceItem()
	//		{
	//			Id = z.Id,
	//			InvoiceId = z.InvoiceId,
	//			ProductId = z.ProductId,
	//			Quantity = z.Quantity
	//		}).ToList()
	//	}).ToList();

	//	return new Pagination<GetAllInvoiceQueryResponse>(list, totalCount, request.Page, request.Limit);
	//}
}
