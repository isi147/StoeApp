using Aplication.CQRS.Invoices.Command.Request;
using Aplication.CQRS.Invoices.Command.Response;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Invoices.Handler.CommandHandlers;

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommandRequest>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteInvoiceCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(DeleteInvoiceCommandRequest request, CancellationToken cancellationToken)
	{
		_unitOfWork.InvoiceRepository.Delete(request.Id);
		await _unitOfWork.SaveChangesAsync();
	}
}
