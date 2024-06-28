using Aplication.CQRS.Products.Command.Request;
using Aplication.CQRS.Products.Command.Response;
using Common.GlobalExceptionsResponses;
using Common.GlobalExceptionsResponses.Generics;
using Domain.Entity;
using MediatR;
using Repository.Common;

namespace Aplication.CQRS.Products.Handler.CommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, ResponseModel<CreateProductCommandResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateProductCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseModel<CreateProductCommandResponse>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
	{
		var newProduct = new Product
		{
			Name = request.Name,
			Description = request.Description,
			Price = request.Price,
			CategoryId = request.CategoryId,
			CreatedDate = DateTime.Now,
			Barcode = request.Barcode
		};
		await _unitOfWork.ProductRepository.AddAsync(newProduct);
		await _unitOfWork.SaveChangesAsync();
		var response = new CreateProductCommandResponse
		{
			Id = newProduct.Id,
			Name = newProduct.Name,
			Description = newProduct.Description,
			Price = newProduct.Price,
			CategoryId = newProduct.CategoryId,
			Barcode = newProduct.Barcode
		};

		return new ResponseModel<CreateProductCommandResponse>(response);
	}

}
