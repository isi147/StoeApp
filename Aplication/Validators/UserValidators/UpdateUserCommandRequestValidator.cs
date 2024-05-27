using Aplication.CQRS.Auth.Command.Request;
using FluentValidation;

namespace Aplication.Validators.UserValidators;

public class UpdateUserCommandRequestValidator : AbstractValidator<UpdateUserCommandRequest>
{

	public UpdateUserCommandRequestValidator()
	{
		RuleFor(request => request.Name)
			.NotEmpty()
			.MaximumLength(50)
			.MinimumLength(0);
		RuleFor(request => request.Surname)
			.NotEmpty()
			.MaximumLength(50)
			.MinimumLength(0);
		RuleFor(request => request.Email)
			.NotEmpty()
			.MaximumLength(50)
			.MinimumLength(0);
	}
}
