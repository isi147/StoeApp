using Aplication.CQRS.Products.Command.Request;
using Aplication.CQRS.Products.Command.Response;
using Common.Exceptions;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Products.Handler.CommandHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
	{
		var currentProduct = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
		if (currentProduct == null)
		{
			throw new BadRequestException();

		}
		currentProduct.Description = request.Description;
		currentProduct.Name = request.Name;
		currentProduct.Price = request.Price;
		currentProduct.CategoryId = request.CategoryId;
		currentProduct.Barcode = request.Barcode;

		_unitOfWork.ProductRepository.Update(currentProduct);
		await _unitOfWork.SaveChangesAsync();

	}
}
