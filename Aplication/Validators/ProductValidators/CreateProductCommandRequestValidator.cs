using Aplication.CQRS.Products.Command.Request;
using FluentValidation;

namespace Aplication.Validators.ProductValidators;

public class CreateProductCommandRequestValidator : AbstractValidator<CreateProductCommandRequest>
{
	public CreateProductCommandRequestValidator()
	{
		RuleFor(request => request.Name)
			.NotEmpty()
			.MaximumLength(50)
			.MinimumLength(0);
		RuleFor(request => request.Description)
			.NotEmpty()
			.MaximumLength(500)
			.MinimumLength(0);
		RuleFor(request => request.Price)
			.NotEmpty()
			.GreaterThan(0);

	}
}
