using Aplication.CQRS.Invoices.Command.Request;
using Aplication.CQRS.Invoices.Command.Response;
using Common.Exceptions;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Invoices.Handler.CommandHandlers;

public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommandRequest, ResponseModel<UpdateInvoiceCommandResponse>>
{
	readonly private IUnitOfWork _unitOfWork;

	public UpdateInvoiceCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<UpdateInvoiceCommandResponse>> Handle(UpdateInvoiceCommandRequest request, CancellationToken cancellationToken)
	{
		var currentInvoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(request.Id);
		if (currentInvoice == null)
		{
			throw new BadRequestException();
		}
		currentInvoice.CashierId = request.CashierId;
		currentInvoice.CustomerId = request.CustomerId;
		currentInvoice.Barcode = request.Barcode;
		currentInvoice.InvoiceType = request.InvoiceType;
		foreach (var item in currentInvoice.InvoiceItems)
		{
			request.InvoiceItems.Add(item);
		}

		_unitOfWork.InvoiceRepository.Update(currentInvoice);
		await _unitOfWork.SaveChangesAsync();
		return new ResponseModel<UpdateInvoiceCommandResponse>();
	}
}
