using Aplication.CQRS.Invoices.Command.Request;
using Aplication.CQRS.Invoices.Command.Response;
using Aplication.DTOs.InvoiceItemDtos;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Invoices.Handler.CommandHandlers;

public class CreateRefundInvoiceCommandHandler : IRequestHandler<CreateRefundInvoiceCommandRequest, ResponseModel<CreateRefundInvoiceCommandResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateRefundInvoiceCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<CreateRefundInvoiceCommandResponse>> Handle(CreateRefundInvoiceCommandRequest request, CancellationToken cancellationToken)
	{
		var currentInvoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(request.SellInvoiceId);

		var newInvoice = new Invoice
		{
			CashierId = request.CashierId,
			CustomerId = request.CustomerId,
			Barcode = request.Barcode,
			InvoiceType = Domain.Enums.InvoiceType.Refund,
			SellInvoiceId = request.SellInvoiceId,
			InvoiceItems = request.InvoiceItems.Select(item => new InvoiceItem
			{
				InvoiceId = item.InvoiceId,
				ProductId = item.ProductId,
				Quantity = item.Quantity,
			}).ToList()
		};

		await _unitOfWork.InvoiceRepository.AddAsync(newInvoice);
		await _unitOfWork.SaveChangesAsync();

		var response = new CreateRefundInvoiceCommandResponse()
		{
			Id = newInvoice.Id,
			CashierId = newInvoice.CashierId,
			CustomerId = newInvoice.CustomerId,
			Barcode = newInvoice.Barcode,
			InvoiceType = newInvoice.InvoiceType,
			InvoiceBarcode = newInvoice.InvoiceBarcode!,
			SellInvoiceId = newInvoice.SellInvoiceId

		};
		response.InvoiceItems.AddRange(newInvoice.InvoiceItems.Select(item =>
			new InvoiceItemRequestDto
			{
				InvoiceId = item.InvoiceId,
				ProductId = item.ProductId,
				Quantity = item.Quantity,

			}
		));

		return new ResponseModel<CreateRefundInvoiceCommandResponse>(response);
	}
}
