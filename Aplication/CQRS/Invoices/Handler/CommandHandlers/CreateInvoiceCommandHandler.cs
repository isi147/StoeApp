using Aplication.CQRS.Invoices.Command.Request;
using Aplication.CQRS.Invoices.Command.Response;
using Aplication.DTOs.InvoiceItemDtos;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Invoices.Handler.CommandHandlers;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommandRequest, ResponseModel<CreateInvoiceCommandResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<CreateInvoiceCommandResponse>> Handle(CreateInvoiceCommandRequest request, CancellationToken cancellationToken)
	{
		var newInvoice = new Invoice
		{
			CashierId = request.CashierId,
			CustomerId = request.CustomerId,
			InvoiceType = Domain.Enums.InvoiceType.Sell,

			InvoiceItems = request.InvoiceItems.Select(item => new InvoiceItem
			{
				InvoiceId = item.InvoiceId,
				ProductId = item.ProductId,
				Quantity = item.Quantity,
			}).ToList()
		};

		await _unitOfWork.InvoiceRepository.AddAsync(newInvoice);
		await _unitOfWork.SaveChangesAsync();


		var response = new CreateInvoiceCommandResponse()
		{
			Id = newInvoice.Id,
			CashierId = newInvoice.CashierId,
			CustomerId = newInvoice.CustomerId,
			Barcode = newInvoice.Barcode,
			InvoiceType = newInvoice.InvoiceType,
			InvoiceBarcode = newInvoice.InvoiceBarcode
		};
		response.InvoiceItems.AddRange(newInvoice.InvoiceItems.Select(item =>
			new InvoiceItemRequestDto
			{
				InvoiceId = item.InvoiceId,
				ProductId = item.ProductId,
				Quantity = item.Quantity
			}
		));

		return new ResponseModel<CreateInvoiceCommandResponse> { Data = response };


	}
}
