using Aplication.CQRS.Categories.Command.Request;
using FluentValidation;

namespace Aplication.Validators.CategoryValidators;

public class CreateCategoryCommandRequestValidator : AbstractValidator<CreateCategoryCommandRequest>
{
	public CreateCategoryCommandRequestValidator()
	{
		RuleFor(request => request.Name)
			.NotEmpty().WithMessage("Cannot be empty")
			.MaximumLength(50)
			.MinimumLength(3);
	}
}
