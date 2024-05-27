using Aplication.CQRS.Invoices.Command.Request;
using FluentValidation;
using Repository.Common;

namespace Aplication.Validators.InvoiceValidators;

public class RefundRequestValidator : AbstractValidator<CreateRefundInvoiceCommandRequest>
{
	private readonly IUnitOfWork _unitOfWork;

	public RefundRequestValidator(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;

		RuleFor(request => request.SellInvoiceId)
			.MustAsync(async (sellInvoiceId, cancellationToken) =>
			{
				var sellInvoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(sellInvoiceId);
				return sellInvoice != null && sellInvoice.Id == sellInvoiceId;
			})
			.WithMessage("SellInvoiceId must match an existing invoice.");

		RuleFor(request => request)
			.MustAsync(async (request, cancellationToken) =>
			{
				var sellInvoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(request.SellInvoiceId);
				return sellInvoice != null && request.InvoiceItems.All(item => sellInvoice.InvoiceItems.Any(i => i.ProductId == item.ProductId));
			})
			.WithMessage("All refunded products must belong to the current invoice.");

		RuleFor(request => request)
			.MustAsync(async (request, cancellationToken) =>
			{
				var sellInvoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(request.SellInvoiceId);
				return sellInvoice != null && request.InvoiceItems.All(item =>
				{
					var initialItem = sellInvoice.InvoiceItems.FirstOrDefault(i => i.ProductId == item.ProductId);
					return initialItem != null && item.Quantity <= initialItem.Quantity;
				});
			})
			.WithMessage("Refunded quantity cannot exceed the initially purchased quantity.");
	}
}
